#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<stdbool.h>
#include<string.h>
#include<stdlib.h>

typedef struct uint1024_t
{
	unsigned char digs[155];
} uint1024_t;

int Find_End(char temp[310])
{
	int k = 309;
	while (!(temp[k] >= '0' && temp[k] <= '9'))
	{
		k--;
	}
	return k;
}

int Find_Nums_Size(uint1024_t* x)
{
	int k = 0;
	while (x->digs[k] == 204)
	{
		k++;
	}
	return 155 - k;
}

bool Is_Zero(uint1024_t *x)
{
	for (int i = 0; i < 155; i++)
	{
		if (x->digs[i] != 0 && x->digs[i] != 204)
		{
			return 0;
		}
	}
	return 1;
}



void scanf_value(uint1024_t* x)
{
	unsigned char temp[310];
	scanf("%s", &temp);
	int k = 154; // Индекс для структуры
	unsigned char temp_char1 = 0;
	unsigned char temp_char2 = 0;
	bool second_digit = true;
	int start = Find_End(temp);
	for (int i = start; i >= 0; --i)
	{
		if (second_digit)
		{
			temp_char1 = temp[i] - '0';
			second_digit = false;
		}
		else if (!second_digit) {
			temp_char2 = (temp[i] - '0') * 10 + temp_char1;
			second_digit = true;
			x->digs[k] = temp_char2;
			k--;
		}
		if (i == 0 && second_digit == false) // Проверка на последнее незаписанное число
		{
			x->digs[k] = temp_char1;
		}
	}
}

uint1024_t from_uint(unsigned int x)
{
	int k = 154; 
	uint1024_t result;
	for (int i = 0; i < 155; i++)
	{
		result.digs[i] = 204;
	}
	bool flag = false;
	unsigned char temp_num = 0;
	while (x > 0)
	{
		if (flag)
		{
			temp_num = (x % 10) * 10 + temp_num;
			x /= 10;
			flag = false;
			result.digs[k] = temp_num;
			k--;
		}
		else {
			temp_num = x%10;
			x /= 10;
			flag = true;
		}
		if (x == 0 && flag) // Проверка на последнее незаписанное число
		{
			result.digs[k] = temp_num;
		}
	}
	return result;
}

void printf_value(uint1024_t* x)
{
	if (Is_Zero(x))
	{
		printf("0");
		return;
	}
	bool First_Digit = true;
	for (int i = 0; i < sizeof(x->digs); ++i)
	{
		if (x->digs[i] > 99)
		{
			continue;
		}
		if (!(First_Digit && (x->digs[i]/10) + '0' == '0')) //  10 63 21 65
		{
			printf("%c", (x->digs[i] / 10) + '0');
			First_Digit = false;
		}
		if (!(First_Digit && (x->digs[i] % 10) + '0' == '0'))
		{
		printf("%c", (x->digs[i] % 10) + '0');
		First_Digit = false;
		}

	}
}

uint1024_t add_op(uint1024_t* x, uint1024_t* y)
{
	uint1024_t result;
	for (int i = 0; i < 155; i++)
	{
		result.digs[i] = 204;
	}
	bool digit = false;
	for (int i = 154; i >= 0 ; --i)
	{
		if (x->digs[i] == 204 && y->digs[i] == 204 && !digit)
		{
			continue;
		}
		else {
			if (x->digs[i] == 204) // x 47382749327985 39 y  59
				x->digs[i] = 0;
			if (y->digs[i] == 204)
				y->digs[i] = 0;
		}
		int temp = x->digs[i] + y->digs[i]+digit;
		if (temp > 99)
		{
			result.digs[i] = temp % 100;
			digit = true;
		}
		else {
			result.digs[i] = temp;
			digit = false;
		}
	}
	return result;
}

uint1024_t sub_op(uint1024_t* x, uint1024_t* y)
{
	uint1024_t result;
	for (int i = 0; i < 155; i++)
	{
		result.digs[i] = 204;
	}
	bool digit = false;
	for (int i = 154; i >= 0; --i)
	{
		if (x->digs[i] == 204 && y->digs[i] == 204)
		{
			continue;
		}
		else {
			if (x->digs[i] == 204) 
				x->digs[i] = 0;
			if (y->digs[i] == 204)
				y->digs[i] = 0;
		}
		int temp = x->digs[i] - y->digs[i] - digit;
		if (temp < 0)
		{
			temp += 100;
			result.digs[i] = temp % 100;
			digit = true;
		}
		else {
			result.digs[i] = temp;
			digit = false;
		}
	}
	return result;
}

uint1024_t mult_op(uint1024_t *x, uint1024_t *y)
{
	uint1024_t result;
	for (int i = 0; i < 155; i++)
	{
		result.digs[i] = 0;
	}
	int x_size = Find_Nums_Size(x);
	int y_size = Find_Nums_Size(y);
	if (y_size > x_size)
	{
		uint1024_t temp = *y;
		*y = *x;
		*x = temp;
	}
	unsigned short int k = 0; // Число сдвига для каждого разряда
	for (int i = 154; i >= 0 ; --i)
	{
		int digit = 0;
		if (y->digs[i] == 204) 
		{
			break;
		}
		for (int j = 154; j >= 0; --j)
		{
			if (x->digs[j] == 204 && digit == 0)
			{
				break;
			}
			else if(x->digs[j] == 204 && digit != 0) {
				result.digs[j-k] += digit;
				digit = 0;
				break;
			}
			unsigned short int temp = x->digs[j] * y->digs[i] + result.digs[j - k] + digit; 
			result.digs[j-k] = temp % 100;
			digit = temp / 100;
		}
		k++;
	}
	for (int i = 0; i < 155; i++)
	{
		if (result.digs[i] != 0)
		{
			break;
		}
		if (result.digs[i] == 0)
		{
			result.digs[i] = 204;
		}
	}
	return result;
}

uint1024_t pow_op(uint1024_t* x, unsigned int degree)
{
	uint1024_t result;
	result.digs[154] = 1;
	while (degree > 0)
	{
		if (degree%2 == 0)
		{
			degree /= 2;
			*x = mult_op(x, x);
		}
		else {
			degree--;
			uint1024_t temp = *x;
			result = mult_op(&result, x);
			*x = temp;
		}
	}
	return result;
}

int main(int argc, char* argv[])
{
	uint1024_t x;
	printf("Input number x: ");
	scanf_value(&x);
	uint1024_t y;
	printf("\nInput number y: ");
	scanf_value(&y);
	uint1024_t r;
	printf("\nYour number x is: ");
	printf_value(&x);
	printf("\nYour number y is: ");
	printf_value(&y);
	r = add_op(&x, &y);
	printf("\n");
	printf("Sum of x and y: ");
	printf_value(&r);
	r = sub_op(&x, &y);
	printf("\n");
	printf("Sub of x and y: ");
	printf_value(&r);
	r = mult_op(&x, &y);
	printf("\n");
	printf("Mult of x and y: ");
	printf_value(&r);
	int degree;
	printf("\nPow of x: ");
	scanf("%d", &degree);
	r = pow_op(&x, degree);
	printf("Number x in ");
	printf("%d", degree);
	printf(" degree: ");
	printf("");
	printf_value(&r);
	printf("\nEnter some number(Not bigger than unsigned int): ");
	unsigned int k;
	scanf("%u", &k);
	uint1024_t num;
	num = from_uint(k);
	printf("\nYour number is: ");
	printf_value(&num);
}