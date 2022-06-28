#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#include"point.h"
#pragma once

class polyline
{
public:
	polyline(const std::vector<point>&other)
	{
		this->points.resize(other.size());
		for (int i = 0; i < (int)other.size(); i++)
		{
			this->points[i] = other[i];
		}
	}

	polyline()
	{
		this->points.resize(1);
		this->points[0] = { 0, 0 };
	}

	polyline(const polyline& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	polyline& operator=(const polyline& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	double get_perimeter() const
	{
		double perimeter = 0;
		if (this->points.size() < 2)
		{
			return 0;
		}
		for (int i = 0; i < (int)this->points.size() - 1; i++)
		{
			point cur, prev;
			prev = this->points[i];
			cur = this->points[i + 1];
			perimeter += sqrt((cur.first - prev.first) * (cur.first - prev.first) + (cur.second - prev.second) * (cur.second - prev.second));
		}
		return perimeter;
	}

private:
	std::vector<point>points;
};