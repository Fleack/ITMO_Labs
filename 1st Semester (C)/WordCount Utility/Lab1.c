#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<stdbool.h>
#include<string.h>
#include<stdlib.h>



void Solution(bool Args[], char* argv[])
{
	FILE* file;
	if ((file = fopen(argv[1], "r")) == NULL)
	{
		printf("File wasn't opened");
		exit(1);
	}
	int String_Cnt = 0;
	int Words_Cnt = 0;
	int Bytes_Cnt = 0;
	bool First_Iteration = true;
	char Current_Symbol;
	char Prev_Symbol;
	while (feof(file) == 0)
	{
		Current_Symbol = fgetc(file);
		if (Current_Symbol != EOF)
		{
			Bytes_Cnt++;
		}
		if (Current_Symbol == '\n' || Current_Symbol == EOF)
		{
			String_Cnt++;
		}
		if (!First_Iteration)
		{
			if (((Current_Symbol == ' ' || Current_Symbol == '\n') && Prev_Symbol != ' ' && Prev_Symbol != '\n') || Current_Symbol == EOF && Prev_Symbol != ' ')
			{
				Words_Cnt++;
			}
		}
		First_Iteration = false;
		Prev_Symbol = Current_Symbol;
	}


	if (Args[0] == true)
	{
		printf("\nCount of lines = ");
		printf("%d", String_Cnt);
	}
	if (Args[1] == true)
	{
		printf("\nCount of words = ");
		printf("%d", Words_Cnt);
	}
	if (Args[2] == true)
	{
		printf("\nCount of bytes = ");
		printf("%d", Bytes_Cnt);
	}
	fclose(file);
}

void print_help()
{
	printf("\n\n\n\n\n");
	printf("Hello, this program can count the number of lines, words and bytes of any file on this computer\n");
	printf("-----------------------------------------------------------------------------------------------\n");
	printf("How to use it?\n");
	printf("1) Type itmo-1-1.exe in cmd\n");
	printf("2) Type name of the file\n");
	printf("3) Type arguments (-l, -w, -b, --lines, --words, --bytes)\n");
	printf("Arguments info:\n");
	printf("-l or --lines will print the number of lines in your file\n");
	printf("-w or --words will print the number of words in your file\n");
	printf("-b or --bytes will print the size of your file in bytes\n");
	printf("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
	printf("!!!ATTENTION: Your file has to be in the same folder as itmo-1-1.exe!!!\n");
	printf("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
	printf("\n\n\n\n\n");
}

int main(int argc, char* argv[])
{
	bool Args[3] = {0, 0, 0 };
	for (int i = 1; i < argc; i++)
	{
		if (strcmp(argv[i], "-h") == 0 || strcmp(argv[i], "--help") == 0)
		{
			print_help();
			exit(0);
		}
		if (strcmp(argv[i], "-l") == 0 || strcmp(argv[i], "--lines"))
		{
			Args[0] = 1;
		}
		if (strcmp(argv[i], "-w") == 0 || strcmp(argv[i], "--words"))
		{
			Args[1] = 1;
		}
		if (strcmp(argv[i], "-b") == 0 || strcmp(argv[i], "--bytes"))
		{
			Args[2] = 1;
		}
	}
	if (Args[0] == 0 && Args[1] == 0 && Args[2] == 0)
	{
		printf("Please, enter some arguments!\n");
		printf("[Program's Name] [File's Name] [Arguments (-l, -w, -b, --lines, --words, --bytes)]");
		exit(0);
	}
	Solution(Args, argv);
}
