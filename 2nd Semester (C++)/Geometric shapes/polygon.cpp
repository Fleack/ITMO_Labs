#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#pragma once

class polygon
{
public:
	polygon(std::vector<std::pair<double, double>>other)
	{
		sort_points(other);
		this->points.resize(other.size());
		for (int i = 0; i < (int)other.size(); i++)
		{
			this->points[i] = other[i];
		}
		if (this->points[0] != this->points[this->points.size() - 1] && this->points.size() > 2)
		{
			this->points.push_back(this->points[0]);
		}
	}

	polygon()
	{
		this->points.resize(1);
		this->points[0] = { 0, 0 };
	}

	polygon(const polygon& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	polygon& operator=(const polygon& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	double get_perimeter()
	{
		double perimeter = 0;
		if (this->points.size() < 2)
		{
			return 0;
		}
		for (int i = 0; i < (int)this->points.size() - 1; i++)
		{
			std::pair<double, double>cur, prev;
			prev = this->points[i];
			cur = this->points[i + 1];
			perimeter += sqrt((cur.first - prev.first) * (cur.first - prev.first) + (cur.second - prev.second) * (cur.second - prev.second));
		}
		return perimeter;
	}

	double get_square()
	{
		double square = 0;
		if (this->points.size() < 3)
		{
			return 0;
		}
		for (int i = 0; i < (int)this->points.size() - 1; i++)
		{
			std::pair<double, double>cur, prev;
			prev = this->points[i];
			cur = this->points[i + 1];
			square += prev.first * cur.second - prev.second * cur.first;
		}
		return (abs(square / 2));
	}
private:
	void sort_points(std::vector<std::pair<double, double>>& other)
	{
		//find center
		if (other[0] == other[other.size() - 1])
		{
			other.pop_back();
		}
		for (int i = 0; i < other.size(); i++)
		{
			center.first += other[i].first;
			center.second += other[i].second;
		}
		center.first /= other.size();
		center.second /= other.size();
		//sort around the center
		for (int i = 0; i < other.size(); i++)
		{
			for (int j = 0; j < other.size() - 1; j++)
			{
				if (less(other[j + 1], other[j]))
				{
					std::swap(other[j + 1], other[j]);
				}
			}
		}
	}

	bool less(const std::pair<double, double> a, const std::pair<double, double> b)
	{
		if (a.first - center.first >= -DBL_EPSILON && b.first - center.first < DBL_EPSILON)
		{
			return true;
		}
		if (a.first - center.first < DBL_EPSILON && b.first - center.first >= -DBL_EPSILON)
		{
			return false;
		}
		if (a.first - center.first < DBL_EPSILON && a.first - center.first > -DBL_EPSILON && b.first - center.first < DBL_EPSILON && b.first - center.first > -DBL_EPSILON)
		{
			if (a.second - center.second >= -DBL_EPSILON || b.second - center.second >= -DBL_EPSILON)
			{
				return a.second > b.second;
			}
			return b.second > a.second;
		}
		double det = (a.first - center.first) * (b.second - center.second) - (b.first - center.first) * (a.second - center.second);
		if (det < 0)
		{
			return true;
		}
		if (det > 0)
		{
			return false;
		}
		double d1 = (a.first - center.first) * (a.first - center.first) + (a.second - center.second) * (a.second - center.second);
		double d2 = (b.first - center.first) * (b.first - center.first) + (b.second - center.second) * (b.second - center.second);
		return d1 > d2;
	}
	std::pair<double, double>center;
	std::vector<std::pair<double, double>>points;
};