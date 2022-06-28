#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#pragma once

class point
{
public:
	point(const double x_set, const double y_set): x(x_set), y(y_set)
	{
	}

	point() : x(0), y(0)
	{
	}

	point(const point& other) : x(other.x), y(other.y)
	{
		this->x = other.x;
		this->y = other.y;
	}

	void set_coordinates(double x_set, double y_set)
	{
		x = x_set;
		y = y_set;
	}

	point& operator=(const point& other)
	{
		this->x = other.x;
		this->y = other.y;
	}

	double get_dist_to_dot(double x2, double y2)
	{
		return sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y));
	}

	double get_dist_to_Ox()
	{
		return abs(y);
	}

	double get_dist_to_Oy()
	{
		return abs(x);
	}
private:
	double x = 0;
	double y = 0;
};
