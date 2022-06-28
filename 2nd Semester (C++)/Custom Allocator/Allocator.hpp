#pragma once
#include <memory>
#include <vector>
#include <algorithm>
#include <stdexcept>
#include <numeric>
#include <limits>



template<class Type>
class allocator
{
public:
    typedef Type value_type;

private:
    struct group
    {
        std::vector<void*>free_chunks;
        mutable size_t size = -1;
    };

public:

    explicit allocator(const std::vector<size_t>& list)
    {
        if (list.empty())
            throw std::logic_error("List can not be empty");

        memory_alloc(list);
    }

    // Базовый конструктор чисто для сравнения производительности с вектором
    explicit allocator()
    {
        std::vector<size_t>params(27);
        size_t s = 2;
        for (int i = 0; i < 27; i++)
        {
            s *= 2;
            params[i] = s;
        }
        memory_alloc(params);
    }

    allocator(const allocator<Type>& other) noexcept :
            memory_begin(other.memory_begin),
            memory_list(other.memory_list)
    {};

    template<typename U>
    explicit allocator(const allocator<U>& other) noexcept
    {
        this->memory_begin = other.memory_begin;
        this->memory_list = other.memory_list;
    }

    ~allocator() = default;

    value_type* allocate(size_t n)
    {
        if (n > std::numeric_limits<std::size_t>::max() / sizeof(value_type))
            throw std::bad_array_new_length();

        for (size_t i = 0; i < (*memory_list).size(); ++i)
        {
            if ((*memory_list)[i].size >= n*sizeof(value_type) && !(*memory_list)[i].free_chunks.empty())
            {
                void* memory_block = (*memory_list)[i].free_chunks[(*memory_list)[i].free_chunks.size() - 1];
                (*memory_list)[i].free_chunks.pop_back();
                return (value_type*)memory_block;
            }
        }
        throw std::bad_alloc();
    }

    void deallocate(value_type* p, size_t size = 0) noexcept
    {
        size_t chunk_size = *((size_t*)p - 1); // Смотрим размер чанка, который принадлежит указателю
        for (size_t i = 0; i < (*memory_list).size(); ++i)
        {
            if ((*memory_list)[i].size == chunk_size)
            {
                (*memory_list)[i].free_chunks.push_back(p);
                return;
            }
        }
    }

    bool operator==(const allocator& other) const noexcept
    {
        return this->memory_list == other.memory_list;
    }

    bool operator!=(const allocator& other) const noexcept
    {
        return (*this) != other;
    }

private:
    std::shared_ptr< std::vector<group> > memory_list;
    std::shared_ptr<void> memory_begin = nullptr;

    // Выделение памяти
    void memory_alloc(const std::vector<size_t>& list)
    {
        // Считаем количество групп и задаем в вектор их кол-во
        std::vector<size_t>params(list);
        std::sort(params.begin(), params.end());
        std::vector<size_t>params_copy(params);
        unsigned long long int groups_cnt = std::unique(params_copy.begin(), params_copy.end()) - params_copy.begin();
        memory_list = std::shared_ptr< std::vector<group> > (new std::vector<group>(groups_cnt));

        // Выделяем память сразу под все параметры
        unsigned long long int total_size_of_memory = std::accumulate(params.begin(), params.end(), 0);
        total_size_of_memory += params.size()*sizeof(size_t);
        memory_begin = std::shared_ptr<void>( malloc( total_size_of_memory), free);
        if (memory_begin == nullptr)
            throw std::bad_alloc();

        int shift = 0; // Сдвиг в памяти
        int i = 0; // Номер группы
        size_t cur_size = params[0]; // Текущий параметр

        for (auto element : params)
        {
            if (cur_size != element)
            {
                // Обозначаем, какие размеры в данной группе
                (*memory_list)[i].size = cur_size;

                // Сдвигаемся в след. группу и меняем текущий размер
                i++;
                cur_size = element;
            }
            *(reinterpret_cast<size_t*> ( (char*)memory_begin.get() + shift )) = cur_size; // Помечаем сколько хранит данный чанк
            (*memory_list)[i].free_chunks.push_back( static_cast<char*>(memory_begin.get()) + shift+sizeof(size_t) ); // Сохраняем чанк в вектор со свободной памятью
            shift += cur_size+ sizeof(size_t);
        }
        (*memory_list)[i].size = cur_size;
    }
};

