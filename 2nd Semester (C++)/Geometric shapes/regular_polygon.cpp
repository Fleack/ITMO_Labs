#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#include<exception>
#pragma once
# define PI           3.14159265358979323846

class regular_polygon
{
public:
	regular_polygon(std::vector<std::pair<double, double>>other)
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
		if (is_regular(other) == false)
		{
			throw std::logic_error("other polygon is not regular");
		}
	}

	regular_polygon()
	{
		this->points.resize(1);
		this->points[0] = { 0, 0 };
	}

	regular_polygon(const regular_polygon& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	regular_polygon& operator=(const regular_polygon& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	bool is_regular(const std::vector<std::pair<double, double>>other)
	{
		this->side_len = 0;
		if (this->points.size() < 2)
		{
			return 0;
		}
		double base_side = sqrt((this->points[1].first - this->points[0].first) * (this->points[1].first - this->points[0].first) + (this->points[1].second - this->points[0].second) * (this->points[1].second - this->points[0].second)); // len of first side
		double base_angle = (((double)this->points.size() - 3) / ((double)this->points.size() - 1)) * 180;
		for (int i = 0; i < (int)this->points.size() - 2; i++)
		{
			std::pair<double, double>point_a1, point_a2, point_a3;
			point_a1 = this->points[i];
			point_a2 = this->points[i + 1];
			point_a3 = this->points[i + 2];
			std::pair<double, double > a1a2 = { point_a1.first - point_a2.first, point_a1.second - point_a2.second };
			std::pair<double, double > a2a3 = { point_a2.first - point_a3.first, point_a2.second - point_a3.second };
			double a1a2_side = sqrt(a1a2.first * a1a2.first + a1a2.second * a1a2.second);
			double a2a3_side = sqrt(a2a3.first * a2a3.first + a2a3.second * a2a3.second);
			double a1a2_a2a3_mult = a1a2.first * a2a3.first + a1a2.second * a2a3.second;
			double cur_angle = acos((a1a2_a2a3_mult) / (a1a2_side * a2a3_side)) * 180 / PI;
			if (a1a2_side != base_side || a2a3_side != base_side)
			{
				return false;
			}
			if (cur_angle != base_angle)
			{
				return false;
			}
		}
		side_len = base_side;
		return true;
	}

	double get_perimeter() const
	{
		return (this->points.size() - 1) * side_len;
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
	void sort_points(std::vector<std::pair<double, double>>&other)
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
			for (int j = 0; j < other.size()-1; j++)
			{
				if (less(other[j+1], other[j]))
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
	double side_len = -1;
};