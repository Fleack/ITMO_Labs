#coding=utf-8
import numpy as np
from scipy.stats import norm

p = 2/3 # Параметр p в распределении Бернулли
epsilon = 0.01 # Заданный эпсилон для отклонения
delta = 0.05 # Заданная дельта для вероятности
n = int(p*(1-p)*1.96/epsilon) # Минимальный объем выборки из ЦПТ
samples_count = 500 # Кол-во выборок
count = 0  # Счётчик количества раз, когда выборочное среднее отличается от мат. ожидания более чем на epsilon

# Генерируем samples_count выборок
for i in range(samples_count):
    sample = np.random.binomial(n=1, p=p, size=n) # Генерируем выборку с распределением Бернулли с параметром p объема n
    sample_mean = np.mean(sample) # Выборочное среднее
    
    # Считаем число выборок, в которых выборочное среднее отличается от мат. ожидания более чем на epsilon
    if abs(sample_mean - p) > epsilon:
        count += 1

print(count)