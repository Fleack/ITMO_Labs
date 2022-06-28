#pragma once
#include<iostream>

// Возведение в степень х
template<int num, int degree>
struct polynom_pow
{
	static constexpr int value = num * polynom_pow<num, degree - 1>::value;
};

// Краевой случай степени 0
template<int num>
struct polynom_pow<num, 0>
{
	static constexpr int value = 1;
};

// Вычисление значения полинома в точке х
template<int x, int degree, int arg1, int... Args>
struct polynom_calc
{
	static constexpr int value = arg1 * polynom_pow<x, degree>::value + polynom_calc<x, degree + 1, Args...>::value;
};

// Вычисление полинома от последнего аргумента
template<int x, int degree, int arg1>
struct polynom_calc<x, degree, arg1>
{
	static constexpr int value = arg1 * polynom_pow<x, degree>::value;
};

// Сам полином для аргументов > 1
template<int arg1, int... Args>
struct polynom
{
	//"метод"(почти) вычисления его значения в точке
	template<int x>
	struct calc_y
	{
		static constexpr int value = arg1 + polynom_calc<x, 1, Args...>::value;
	};
};

// Полином для 1 аргумента
template<int arg1>
struct polynom<arg1>
{
	//"метод"(почти) вычисления его значения в точке
	template<int x>
	struct calc_y
	{
		static constexpr int value = arg1;
	};
};

// Функция для вывода полинома с кол-вом аргументов > 1
template<int arg1, int ... Args>
void polynom_print(std::ostream& out, int degree, const polynom<arg1, Args...>& poly)
{
	if (arg1 >= 0)
		out << '+';
	out << arg1 << "*x^" << degree;
	polynom_print(out, degree + 1, polynom<Args...>());
}

// Функция для вывода последнего аргумента полинома
template<int arg1>
void polynom_print(std::ostream& out, int degree, const polynom<arg1>& poly)
{
	if (arg1 >= 0)
		out << '+';
	out << arg1 << "*x^" << degree;
}

// Вывод полинома с > 1 аргументов
template<int arg1, int ... Args>
std::ostream& operator<<(std::ostream& out, const polynom<arg1, Args...>& poly)
{
	out << arg1;
	polynom_print(out, 1, polynom<Args...>());
	return out;
}

// Вывод полинома из 1 аргумента
template<int arg1>
std::ostream& operator<<(std::ostream& out, const polynom<arg1>& poly)
{
	out << arg1;
	return out;
}
