#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<string.h>
#include<time.h>
#include<malloc.h>
#include<stdbool.h>
#include<stdlib.h>

FILE* file;
FILE* err_file;
int lines_cnt = 0;
int Errors_5xx = 0;
time_t window = -1;
int max_reqs = -1;
int cur_req = 0;

typedef struct
{
	int error_id;
	char* req;
	struct request* next;
	time_t abs_time;
	struct tm req_time;
} request;
request* head = NULL;
request* tail = NULL;

void req_pop()
{
	if (cur_req > 1)
	{
		cur_req--;
	}
	if (head == NULL)
	{
		exit(777);
	}
	if (head == tail)
	{
		free(head);
		head = NULL;
		tail = NULL;
	}
	else {
		request* pRequest = head->next;
		head->next = NULL;
		free(head);
		head = pRequest;
	}
}

void req_push(request *New_Request)
{
	request* pRequest = New_Request;
	if (head == NULL)
	{
		head = pRequest;
		tail = pRequest;
	}
	else
	{
		tail->next = pRequest;
		tail = pRequest;
	}
	if (lines_cnt > 936450)
	{
		max_reqs += 0;
	}
	cur_req++;
	if (difftime(tail->abs_time,head->abs_time) <= window)
	{
		if (max_reqs < cur_req)
		{
			max_reqs = cur_req;
			
		}
	}
	else
	{
		while (difftime(tail->abs_time, head->abs_time) > window)
		{
			req_pop();
		}
	}
}

void Parsing()
{
	request* New_Request = (request*)malloc(sizeof(request));
	char cur = fgetc(file);
	bool req_part = false;
	char request_str[1000]; int request_index = 0;
	bool req_time = false;
	char request_time_str[50]; int time_index = 0;
	bool req_error = false;
	int error = 0;
	while (cur != '\0' && cur != EOF && cur != '\n')
	{
		if (cur == '[')
		{
			req_time = true;
			cur = fgetc(file);
			continue;
		}
		else if (cur == ']')
		{
			req_time = false;
			cur = fgetc(file);
			continue;
		}
		else if (cur == '"')
		{
			req_part = !req_part;
			if (req_part == false)
			{
				req_error = true;
			}
			cur = fgetc(file);
			continue;
		}
		if (req_part)
		{
			request_str[request_index] = cur;
			request_index++;
		}
		if (req_time)
		{
			request_time_str[time_index] = cur;
			time_index++;
		}
		if (req_error)
		{
			cur = fgetc(file);
			error = cur - '0';
			cur = fgetc(file);
			error = error * 10 + cur - '0';
			cur = fgetc(file);
			error = error * 10 + cur - '0';
			req_error = false;
		}
		cur = fgetc(file);
	}
	New_Request->req_time.tm_mday = (request_time_str[0] - '0') * 10 + request_time_str[1] - '0';
	char month[4]; month[0] = request_time_str[3]; month[1] = request_time_str[4]; month[2] = request_time_str[5]; month[3] = '\0';
	if (strcmp(month, "Jan") == 0)
		New_Request->req_time.tm_mon = 0;
	else if (strcmp(month, "Feb") == 0)
		New_Request->req_time.tm_mon = 1;
	else if (strcmp(month, "Mar") == 0)
		New_Request->req_time.tm_mon = 2;
	else if (strcmp(month, "Apr") == 0)
		New_Request->req_time.tm_mon = 3;
	else if (strcmp(month, "May") == 0)
		New_Request->req_time.tm_mon = 4;
	else if (strcmp(month, "Jun") == 0)
		New_Request->req_time.tm_mon = 5;
	else if (strcmp(month, "Jul") == 0)
		New_Request->req_time.tm_mon = 6;
	else if (strcmp(month, "Aug") == 0)
		New_Request->req_time.tm_mon = 7;
	else if (strcmp(month, "Sep") == 0)
		New_Request->req_time.tm_mon = 8;
	else if (strcmp(month, "Oct") == 0)
		New_Request->req_time.tm_mon = 9;
	else if (strcmp(month, "Nov") == 0)
		New_Request->req_time.tm_mon = 10;
	else if (strcmp(month, "Dec") == 0)
		New_Request->req_time.tm_mon = 11;
	else
	{
		fprintf(err_file, "Wrong log on the line %d!", lines_cnt);
	}
	New_Request->req_time.tm_year = ((request_time_str[7] - '0') * 1000 + (request_time_str[8] - '0')*100+(request_time_str[9] - '0') * 10 + request_time_str[10] - '0')-1900;
	New_Request->req_time.tm_hour = (request_time_str[12] - '0') * 10 + request_time_str[13] - '0';
	New_Request->req_time.tm_min = (request_time_str[15] - '0') * 10 + request_time_str[16] - '0';
	New_Request->req_time.tm_sec = (request_time_str[18] - '0') * 10 + request_time_str[19] - '0';
	char* req_str = calloc(request_index+1, sizeof(char));
	int k = 0;
	while (k < request_index)
	{
		req_str[k] = request_str[k];
		k++;
	}
	New_Request->req = req_str;
	New_Request->error_id = error;
	if (error / 100 == 5)
	{
		Errors_5xx++;
		fprintf(err_file, "Time: %d/%s/%d:%d:%d:%d		", New_Request->req_time.tm_mday, month, New_Request->req_time.tm_year, New_Request->req_time.tm_hour, New_Request->req_time.tm_min, New_Request->req_time.tm_sec);
		fprintf(err_file, "Request: \"%s\"		", New_Request->req);
		fprintf(err_file, "Error id: %d	\n", New_Request->error_id);
	}
	New_Request->abs_time = mktime(&New_Request->req_time);
	free(req_str);
	req_push(New_Request);
} // Идея - докидывать в список запрос, проверять, входит ли он ещё во временное окно, если нет, то двигаем левую часть окна(первый элемент в списке), если да, то всё ок продолжаем пушить дальше. Параллельно ведем переменную макс, и текущее кол-во эл в списке. При пуше увеличиваем на 1 после проверки, на временное окно, уменьшаем на 1 после попа




int main(int argc, char* argv[])
{
	if (argc < 2)
	{
		printf("Plese, enter file's name!");
		exit(1);
	}
	if ((file = fopen(argv[1], "r")) == NULL)
	{
		printf("File wasn't opened");
		exit(1);
	}
	char format;
	int format_num;
	printf("\nPlease enter a time window in the format \"*@\", where \"*\" is a natural integer, and @ is a unit of time [Seconds - s, Minutes - m, Hours - h].");
	printf("\nFor example:");
	printf("\n\"6s\" means 6 seconds");
	printf("\n\"12m\" means 12 minutes");
	printf("\n\"24h\" means 24 hours\n");
	printf("Enter a time window: ");
	scanf("%d", &format_num);
	scanf("%c", &format);
	if (format == 'h')
	{
		window = (long long int)format_num * 60 * 60;
	}
	else if (format == 'm') {
		window = (long long int)format_num * 60;
	}
	else if (format == 's')
	{
		window = (long long int)format_num;
	}
	else
	{
		printf("Wrong window format, plese enter the correct one");
		exit(101);
	}
	err_file = fopen("Errors.txt", "w");
	while (feof(file) == 0)
	{
		lines_cnt++;
		Parsing();
	}
	fprintf(err_file, "\n\nTotal errors num - %d", Errors_5xx);
	printf("Max request - %d", max_reqs);
	fclose(file);
	fclose(err_file);
}