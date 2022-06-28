#include "Allocator.hpp"
#include <iostream>
#include <vector>
#include <ctime>
#include <map>

int main()
{
    // Кастомный аллокатор
    clock_t custom_alloc_result;
    {
        allocator<int> alloc;
        std::vector<int, allocator<int>>vector_with_custom_allocator(0, alloc);
        clock_t custom_alloc_start_time = clock();
        for (int i = 0; i < 50000000; i++)
        {
            vector_with_custom_allocator.push_back(rand());
        }
        clock_t custom_alloc_end_time = clock();
        custom_alloc_result = custom_alloc_end_time - custom_alloc_start_time;
        std::cout << custom_alloc_result << "\n\n\n";
    }


    // Обычный аллокатор
    clock_t regular_allocator_result;
    {
        std::vector<int>vector_with_regular_allocator(0);
        clock_t regular_allocator_start_time = clock();
        for (int i = 0; i < 50000000; i++)
        {
            vector_with_regular_allocator.push_back(rand());
        }
        clock_t regular_allocator_end_time = clock();
        regular_allocator_result = regular_allocator_end_time - regular_allocator_start_time;
        std::cout << regular_allocator_result << "\n\n\n";
    }

    // Сравнение производительности
    std::cout << "Custom allocator " << (double)custom_alloc_result/(double)regular_allocator_result << " times faster with vector!\n";
    return 0;
}