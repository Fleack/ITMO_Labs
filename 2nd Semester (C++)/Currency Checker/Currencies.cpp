#define CURL_STATICLIB
#include <iostream>
#include <stdlib.h>
#include <algorithm>
#include "windows.h"
#include "curl/curl.h"
#include "single_include/nlohmann/json.hpp"
#define _CRT_SECURE_NO_WARNINGS
using json = nlohmann::json;

class all_currencies
{
public:
	std::map<std::string, std::vector<double>> valutes;
	int cnt = 0;
}; 
all_currencies curr;

size_t write_data(char* c_ptr, size_t size, size_t elements_cnt, std::string* data)
{
	if (data)
	{
		data->append(c_ptr, size * elements_cnt);
		return size * elements_cnt;
	}
	else
	{
		return 0;
	}
}

char is_bigger(double cur, double prev)
{
	if (cur > prev)
	{
		return '+';
	}
	if (prev > cur)
	{
		return '-';
	}
	return ' ';
}

void output_currencies(json& json)
{
	std::cout << "Date: ";
	std::cout << json["Date"];
	std::cout << "\nExchange rates:\n\n";
	std::cout << "Currency " << " Nominal " << "Exchange rate " << "Prev exchange rate\n\n";
	int i = 0;
	curr.cnt++;
	for (const auto &str : json["Valute"])
	{
		std::cout << str["CharCode"] << "\t";
		std::cout << str["Nominal"] << "\t";
		std::cout << str["Value"] << is_bigger(str["Value"], str["Previous"]) << "\t";
		std::cout << str["Previous"] << '\n';
		curr.valutes[str["CharCode"]].push_back(str["Value"]); // Ñîõðàíÿåì ñòîèìîñòü âàëþòû
		i++;
		//std::cout << json["Valute"][str]["Name"] << '\n'; Doesn't work, because everything is in russian...
	}
}

void get_currencies()
{
	CURL* curl;
	curl = curl_easy_init();
	if (curl != nullptr)
	{
		const std::string site = "https://www.cbr-xml-daily.ru/daily_json.js";
		std::string data;
		curl_easy_setopt(curl, CURLOPT_URL, site.c_str()); // Óêàçàëè ñàéò API
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &data); // Óêàçàëè áóôåð äëÿ çàïèñè
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, write_data); // Óêàçàòü ôóêíöèþ äëÿ çàïèñè â áóôåð
		CURLcode res = curl_easy_perform(curl);
		if (res != CURLE_OK) // Ïðîâåðèëè íå âîçíèêëî ëè îøèáîê
		{
			std::cout << "Âî âðåìÿ ðàáîòû âîçíèêëà ñëåäóþùàÿ îøèáêà CURL: \n" << curl_easy_strerror(res) << '\n';
			exit(-1);
		}
		curl_easy_cleanup(curl);
		json currencies;
		currencies = json::parse(data); // Ïàðñèì ñòðîêó
		output_currencies(currencies);
	}
	else
	{
		throw("Failed to start CURL");
	}
}

void get_avg_and_mid()
{
	std::cout << "\n\n\nValute\tAvg\tMid\n\n";
	std::map<std::string, std::vector<double>>::iterator iter = curr.valutes.begin();
	while (iter != curr.valutes.end())
	{
		double avg = 0;
		double mid = 0;
		for (int i = 0; i < iter->second.size(); i++) // Áåæèì ïî ìàññèâó èñòîðèè êóðñà òåêóùåé âàëþòû
		{
			avg += iter->second[i];
		}
		avg /= curr.cnt;
		std::sort(iter->second.begin(), iter->second.end());
		if (curr.cnt % 2 == 0)
		{
			mid += iter->second[(curr.cnt / 2)];
			mid += iter->second[(curr.cnt / 2) - 1];
			mid /= 2;
		}
		else
		{
			mid = iter->second[(curr.cnt / 2)];
		}
		std::cout << iter->first << '\t' << avg << '\t' << mid << '\n';
		iter++;
	}
}

int main()
{
	int prog_cnts;
	std::cin >> prog_cnts;
	int cur_cnt = 0;
	while (prog_cnts > cur_cnt)
	{
		system("cls");
		get_currencies();
		Sleep(1000 * 10);
		cur_cnt++;
	}
	get_avg_and_mid();
	return 0;
}