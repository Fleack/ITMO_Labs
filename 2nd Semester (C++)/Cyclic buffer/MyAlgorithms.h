#pragma once
#include <stdexcept>


template<typename iter, class UnaryPredicate>
bool all_of(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	while (begin != end)
	{
		if (!pred(*begin))
		{
			return false;
		}
		++begin;
	}
	return true;
}

template<typename iter, class UnaryPredicate>
bool any_of(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	while (begin != end)
	{
		if (pred(*begin))
		{
			return true;
		}
		++begin;
	}
	return false;
}

template<typename iter, class UnaryPredicate>
bool none_of(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	if (any_of(begin, end, pred))
	{
		return false;
	}
	return true;
}

template<typename iter, class UnaryPredicate>
bool one_of(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	unsigned int cnt = 0;
	while (begin != end)
	{
		if (pred(*begin))
		{
			cnt++;
		}
		++begin;
	}
	if (cnt == 1)
		return true;
	return false;
}

template<typename iter, class UnaryPredicate>
bool is_sorted(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	while (begin + 1 != end)
	{
		if (!pred(*begin, *begin + 1))
		{
			return false;
		}
		++begin;
	}
	return true;
}

template<typename iter, class UnaryPredicate>
bool is_partitioned(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	while (begin != end && pred(*begin))
	{
		++begin;
	}
	while (begin != end)
	{
		if (pred(*begin))
		{
			return false;
		}
		++begin;
	}
	return true;
}

template<typename iter, class UnaryPredicate>
bool is_palindrome(iter begin, iter end, UnaryPredicate pred)
{
	correct_iter(begin, end);
	iter it_begin = begin;
	iter it_end = (--end);
	while (it_begin < end && it_end >= begin)
	{
		if (it_begin != it_end)
		{
			return false;
		}
		it_begin++;
		it_end--;
	}
	return true;
}

template<typename iter, typename Type>
iter find_not(iter begin, iter end, Type& element)
{
	correct_iter(begin, end);
	while (begin != end)
	{
		if (*begin != element)
		{
			break;
		}
		++begin;
	}
	return begin;
}

template<typename iter, typename Type>
iter find_backward(iter begin, iter end, Type& element)
{
	correct_iter(begin, end);
	iter it = (--end);
	while (it >= begin)
	{
		if (*it == element)
		{
			return it;
		}
		--it;
	}
	return end;
}

template<typename iter>
void correct_iter(iter& a, iter& b)
{
	if (b > a)
	{
		throw std::invalid_argument("b has to be < a");
	}
}
