#pragma once
#include<iterator>
#include <stdexcept>
#define MIN_SIZE 2

template<typename Type>
class circular_buffer
{
public:
	class iter : public std::iterator<std::random_access_iterator_tag, Type>
	{
	public:
		iter(Type* other, Type* begin, Type* end) :
			ptr(other),
			buffer_begin(begin),
			buffer_end(end)
		{}

		iter() :
			ptr(nullptr),
			buffer_begin(nullptr),
			buffer_end(nullptr)
		{}

		void init(Type* other, Type* begin, Type* end)
		{
			ptr = other;
			buffer_begin = begin;
			buffer_end = end;
		}

		void swap(iter& other)
		{
			std::swap(*this, iter);
		}

		iter& operator= (const iter& other)
		{
			ptr = other.ptr;
			buffer_begin = other.buffer_begin;
			buffer_end = other.buffer_end;
			return *this;
		}

		iter& operator= (const Type& other)
		{
			*ptr = other;
			return *this;
		}

		bool operator== (const iter& other) const
		{
			return ptr == other.ptr;
		}

		bool operator!= (const iter& other) const
		{
			return ptr != other.ptr;
		}

		bool operator>=(const iter& other) const
		{
			return ptr >= other.ptr;
		}

		bool operator<=(const iter& other) const
		{
			return ptr <= other.ptr;

		}

		bool operator>(const iter& other) const
		{
			return ptr > other.ptr;
		}

		bool operator<(const iter& other) const
		{
			return ptr < other.ptr;
		}

		iter& operator- (const int n)
		{
			if (this->ptr - n >= buffer_begin)
			{
				this->ptr -= n;
			}
			else
			{
				this->ptr = buffer_end - (n - (this->ptr - buffer_begin));
			}
			return *this;
		}

		iter& operator+ (const int n)
		{
			if (this->ptr + n <= buffer_end)
			{
				this->ptr += n;
			}
			else
			{
				this->ptr = buffer_begin + (n - (buffer_end - this->ptr));
			}
			return *this;
		}

		iter& operator++() // prefix++
		{
			if (this->ptr + 1 <= buffer_end)
			{
				this->ptr += 1;
			}
			else
			{
				this->ptr = buffer_begin;
			}
			return *this;
		}

		iter& operator++(Type) // postfix++
		{
			if (this->ptr + 1 <= buffer_end)
			{
				this->ptr += 1;
			}
			else
			{
				this->ptr = buffer_begin;
			}
			return *this;
		}

		iter& operator--() // prefix++
		{
			if (this->ptr - 1 >= buffer_begin)
			{
				this->ptr -= 1;
			}
			else
			{
				this->ptr = buffer_end - 1;
			}
			return *this;
		}

		iter& operator--(Type) // postfix++
		{
			if (this->ptr - 1 >= buffer_begin)
			{
				this->ptr -= 1;
			}
			else
			{
				this->ptr = buffer_end - 1;
			}
			return *this;
		}

		Type& operator* () const
		{
			return *ptr;
		}

		Type& operator-> () const
		{
			return *ptr;
		}

		Type& operator[] (const int index) const
		{
			return *(ptr + (index % buffer_size));
		}

	private:
		Type* ptr;
		Type* buffer_begin;
		Type* buffer_end;
	};

	

	circular_buffer(const size_t size) :
		buffer_size(size),
		capacity(size * 2),
		capacity_begin(new Type[size * 2])
	{
		capacity_end = capacity_begin + size * 2;
		buffer_begin.init(capacity_begin, capacity_begin, capacity_begin + size);
		buffer_end.init(capacity_begin + size, capacity_begin + size, capacity_begin + size);
		data_begin = buffer_begin.ptr;
		data_end = data_begin + size - 1;
	}

	circular_buffer(std::initializer_list<Type> list) : // init list
		circular_buffer(list.size())
	{
		int i = 0;
		for (auto element : list)
		{
			capacity_begin[i] = element;
			i++;
		}
	}

	circular_buffer() :
		capacity(0),
		buffer_size(0),
		capacity_begin(nullptr),
		capacity_end(nullptr),
		data_begin(nullptr),
		data_end(nullptr),
		buffer_begin(nullptr, nullptr, nullptr),
		buffer_end(nullptr, nullptr, nullptr)
	{
	}

	~circular_buffer()
	{
		delete[] capacity_begin;
	}

	void pop_front()
	{
		if (buffer_size == 0)
		{
			throw std::runtime_error("buffer is empty");
		}
		if (data_end < data_begin)
		{
			normalize();
		}
		iter it = buffer_begin;
		while (it != buffer_end)
		{
			*it = *(it.ptr + 1);
			it++;
		}
		if (data_end == data_begin)
		{
			data_end = nullptr;
		}
		else
		{
			data_end--;
		}
		buffer_end.ptr--;
		buffer_end.buffer_end--;
		buffer_begin.buffer_end--;
		buffer_size--;
	}

	void pop_back()
	{
		if (buffer_size == 0)
		{
			throw std::runtime_error("buffer is empty");
		}
		if (data_end < data_begin)
		{
			normalize();
		}
		if (data_end == data_begin)
		{
			data_end = nullptr;
		}
		else
		{
			data_end--;
		}
		buffer_end.ptr--;
		buffer_end.buffer_end--;
		buffer_begin.buffer_end--;
		buffer_size--;
	}

	void push_back(const Type value)
	{
		if (capacity == 0)
		{
			reserve(MIN_SIZE);
			*data_end = value;
			buffer_end.ptr++;
			buffer_begin.buffer_end++;
			buffer_end.buffer_end++;
			buffer_size++;
		}
		else
		{
			if (buffer_size == capacity)
			{
				reserve(capacity * 2);
			}
			else if (data_end != nullptr && data_end < data_begin)
			{
				normalize();
			}
			if (data_end == nullptr)
			{
				data_end = data_begin;
			}
			else
			{
				data_end++;
			}
			*data_end = value;
			buffer_end.ptr++;
			buffer_begin.buffer_end++;
			buffer_end.buffer_end++;
			buffer_size++;
		}
	}

	void push_front(const Type value)
	{
		if (capacity == 0)
		{
			reserve(2);
			buffer_begin.ptr = capacity_begin;
			buffer_end.ptr = capacity_begin + 1;
			data_begin = capacity_begin;
			data_end = capacity_begin;
			*data_end = value;
			buffer_size++;
			buffer_begin.buffer_begin = capacity_begin;
			buffer_end.buffer_begin = capacity_begin;
			buffer_begin.buffer_end = capacity_begin + 1;
			buffer_end.buffer_end = capacity_begin + 1;
		}
		else
		{
			if (buffer_size == capacity)
			{
				reserve(capacity * 2);
			}
			else if (data_end != nullptr && data_end < data_begin)
			{
				normalize();
			}
			data_begin = buffer_end.ptr;
			if (data_end == nullptr)
			{
				data_end = data_begin;
			}
			*data_begin = value;
			buffer_end.ptr++;
			buffer_size++;
			buffer_begin.buffer_end++;
			buffer_end.buffer_end++;
		}
	}

	Type& operator[] (const int n)
	{
		return *(capacity_begin + n);
	}

	int size() const
	{
		return buffer_size;
	}

	iter begin()
	{
		if (data_end != nullptr && data_begin > data_end)
		{
			normalize();
		}
		return buffer_begin;
	}

	iter end()
	{
		if (data_end != nullptr && data_begin > data_end)
		{
			normalize();
		}
		return buffer_end;
	}

	void reserve(size_t new_capacity)
	{
		if (new_capacity <= capacity)
		{
			return;
		}
		Type* new_buffer = new Type[new_capacity];
		iter temp_iter(new_buffer, new_buffer, new_buffer + new_capacity);
		iter begin(data_begin, buffer_begin.ptr, buffer_end.ptr);
		iter end(data_end, buffer_begin.ptr, buffer_end.ptr);

		while (data_end != nullptr && begin != end)
		{
			if (begin != buffer_end)
			{
				*temp_iter = *begin;
				temp_iter++;
			}
			begin++;
		}

		delete[] capacity_begin;

		capacity_begin = new_buffer;
		capacity_end = new_buffer + new_capacity;
		capacity = new_capacity;
		buffer_begin.init(capacity_begin, capacity_begin, capacity_begin + buffer_size);
		buffer_end.init(capacity_begin + buffer_size, capacity_begin, capacity_begin + buffer_size);
		data_begin = capacity_begin;
		if (buffer_size == 0)
		{
			data_end = data_begin;
		}
		else
		{
			data_end = capacity_begin + buffer_size - 1;
		}
	}

private:

	void normalize()
	{
		Type* new_buffer = new Type[buffer_size];
		iter temp_iter(new_buffer, new_buffer, new_buffer + buffer_size);
		iter begin(data_begin, buffer_begin.ptr, buffer_end.ptr);
		iter end(data_end, buffer_begin.ptr, buffer_end.ptr);

		while (data_end != nullptr && begin != end)
		{
			if (begin != buffer_end)
			{
				*temp_iter = *begin;
				temp_iter++;
			}
			begin++;
		}

		begin.init(capacity_begin, capacity_begin, capacity_begin + buffer_size);
		end.init(capacity_begin + buffer_size, capacity_begin, capacity_begin + buffer_size);
		temp_iter.init(new_buffer, new_buffer, new_buffer + buffer_size);

		while (begin != end)
		{
			*begin = *temp_iter;
			begin++;
			temp_iter++;
		}
		buffer_begin.init(capacity_begin, capacity_begin, capacity_begin + buffer_size);
		buffer_end.init(capacity_begin + buffer_size, capacity_begin, capacity_begin + buffer_size);
		data_begin = capacity_begin;
		if (buffer_size == 0)
		{
			data_end = data_begin;
		}
		else
		{
			data_end = capacity_begin + buffer_size - 1;
		}
	}



	Type* data_begin;
	Type* data_end;
	iter buffer_begin;
	iter buffer_end;
	mutable Type* capacity_begin;
	mutable Type* capacity_end;
	size_t capacity;
	size_t buffer_size;
};