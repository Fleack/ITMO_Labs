#include <iostream>
#include <vector>

class polynomial
{
public:

	polynomial()
	{
		this->_coefs = { 0 };
	}

	polynomial(const std::vector<double>&coefs)
	{
		this->_coefs.resize(coefs.size());
		for (int i = 0; i < coefs.size(); i++)
		{
			this->_coefs[i] = coefs[i];
		}
	}

	polynomial(const polynomial& other)
		: _coefs(other._coefs)

	{
	}

	~polynomial()
	{
	}

	void vector_init(const std::vector<double>& coefs) 
	{
		this->_coefs = coefs;
	}

	polynomial& operator=(const polynomial& other)
	{
		if (&other == this)
		{
			return *this;
		}
		_coefs.resize(other._coefs.size());
		for (int i = 0; i < _coefs.size(); i++)
		{
			this->_coefs[i] = other._coefs[i];
		}
		return *this;
	}

	bool operator==(const polynomial& other)
	{
		if (this->_coefs.size() != other._coefs.size())
		{
			return false;
		}
		else
		{
			for (int i = 0; i < _coefs.size(); i++)
			{
				if (this->_coefs[i] != other._coefs[i])
				{
					return false;
				}
			}
		}
		return true;
	}

	bool operator!=(const polynomial& other) const
	{
		return !(*this == other);
	}

	polynomial operator+(const polynomial& other) const
	{
		*this += other;
	}

	polynomial operator-(const polynomial& other)
	{
		polynomial inv(other);
		for (int i = 0; i < inv._coefs.size(); i++)
		{
			inv._coefs[i] *= -1;
		}
		int temp_index;
		int index;
		polynomial temp;
		if (inv._coefs.size() > this->_coefs.size())
		{
			temp = inv;
			temp_index = inv._coefs.size() - 1;
			index = this->_coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += this->_coefs[index];
				temp_index--;
				index--;
			}
		}
		else
		{
			temp = *this;
			temp_index = this->_coefs.size() - 1;
			index = inv._coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += inv._coefs[index];
				temp_index--;
				index--;
			}
		}
		return temp;

	}

	polynomial operator*(const double multiplier)
	{
		polynomial temp(*this);
		for (int i = 0; i < temp._coefs.size(); i++)
		{
			temp._coefs[i] *= multiplier;
		}
		return temp;
	}

	polynomial operator*(const polynomial& other)
	{
		std::vector<double>res_coefs(this->_coefs.size() + other._coefs.size() - 1);
		for (int i = 0; i < res_coefs.size(); i++)
		{
			res_coefs[i] = 0;
		}
		polynomial result(res_coefs);
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			for (int j = 0; j < other._coefs.size(); j++)
			{
				result._coefs[i + j] += this->_coefs[i] * other._coefs[j];
			}
		}
		return result;
	}

	polynomial operator/(const double divider)
	{
		if (divider == 0)
		{
			throw "Can't devide by zero";
		}
		polynomial temp(*this);
		for (int i = 0; i < temp._coefs.size(); i++)
		{
			temp._coefs[i] /= divider;
		}
		return temp;
	}

	void operator+=(const polynomial& other)
	{
		int index;
		int temp_index;
		polynomial temp;
		if (other._coefs.size() > this->_coefs.size())
		{
			temp = other;
			temp_index = other._coefs.size() - 1;
			index = this->_coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += this->_coefs[index];
				temp_index--;
				index--;
			}
		}
		else
		{
			temp = *this;
			temp_index = this->_coefs.size() - 1;
			index = other._coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += other._coefs[index];
				temp_index--;
				index--;
			}
		}
		this->_coefs = temp._coefs;
	}

	void operator-=(const polynomial& other)
	{
		polynomial inv(other);
		for (int i = 0; i < inv._coefs.size(); i++)
		{
			inv._coefs[i] *= -1;
		}
		int temp_index;
		int index;
		polynomial temp;
		if (inv._coefs.size() > this->_coefs.size())
		{
			temp = inv;
			temp_index = inv._coefs.size() - 1;
			index = this->_coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += this->_coefs[index];
				temp_index--;
				index--;
			}
		}
		else
		{
			temp = *this;
			temp_index = this->_coefs.size() - 1;
			index = inv._coefs.size() - 1;
			while (index >= 0)
			{
				temp._coefs[temp_index] += inv._coefs[index];
				temp_index--;
				index--;
			}
		}
		this->_coefs = temp._coefs;
	};

	void operator*=(const double multiplier)
	{
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			this->_coefs[i] *= multiplier;
		}
	}

	void operator*=(const polynomial& other)
	{
		std::vector<double>res_coefs(this->_coefs.size() + other._coefs.size() - 1);
		for (int i = 0; i < res_coefs.size(); i++)
		{
			res_coefs[i] = 0;
		}
		polynomial result(res_coefs);
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			for (int j = 0; j < other._coefs.size(); j++)
			{
				result._coefs[i + j] += this->_coefs[i] * other._coefs[j];
			}
		}
		this->_coefs = result._coefs;
	}

	void operator/=(const double divider)
	{
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			this->_coefs[i] /= divider;
		}
	}

	polynomial& operator++() //prefix++
	{
		return *this;
	}

	polynomial& operator++(const int postfix) //postfix++
	{
		return *this;
	}

	polynomial& operator--() //prefix--
	{
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			_coefs[i] *= -1;
		}
		return *this;
	}

	polynomial& operator--(const int postfix) //postfix--
	{
		for (int i = 0; i < this->_coefs.size(); i++)
		{
			_coefs[i] *= -1;
		}
		return *this;
	}

	double operator[](const int index)
	{
		if (index >= this->_coefs.size() || index < 0)
		{
			throw "Out of range";
		}
		return this->_coefs[index];
	}
	private:
		std::vector<double>_coefs;
		friend std::ostream& operator<<(std::ostream& s, const polynomial& other);
		friend std::istream& operator>>(std::istream& in, const polynomial& other);
};



std::ostream& operator<<(std::ostream& out, const polynomial& other)
{
	if (other._coefs.size() == 1 && other._coefs[0] == 0)
	{
		std::cout << '0';
	}
	for (int i = 0; i < other._coefs.size(); i++)
	{
		if (other._coefs[i] != 0)
		{
			if (i != 0)
			{
				if (other._coefs[i] > 0)
				{
					out << '+';
				}
			}
			if (other._coefs[i] != 1)
			{
				if (other._coefs[i] == -1)
				{
					out << '-';
				}
				else
				{
					out << other._coefs[i];
				}
			}
			if (i + 1 != other._coefs.size())
			{
				out << "x";
				if (other._coefs.size() - 1 - i > 1)
				{
					out << '^' << other._coefs.size() - 1 - i;
				}
			}
		}
	}
	out << '\n';
	return out;
}

std::istream& operator>>(std::istream& in, polynomial& other)
{
	int polynomial_size;
	in >> polynomial_size;
	std::vector<double>temp(polynomial_size);
	for (int i = 0; i < polynomial_size; i++)
	{
		in >> temp[i];
	}
	other.vector_init(temp);
	return in;
}