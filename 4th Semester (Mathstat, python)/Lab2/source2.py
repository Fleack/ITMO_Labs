#coding=utf-8
import numpy as np
import matplotlib.pyplot as plt
from scipy.stats import gamma

# Задаем параметры распределения
theta = 2
k = 3

# Задаем параметры выборок
sizes = [50, 100, 500, 1000, 2500]
repeats = 500

# Инициализируем массивы для сохранения результатов для каждой выборки
results = np.zeros((len(sizes), repeats))

# Генерируем выборки и оцениваем параметр для каждой выборки
for i, n in enumerate(sizes):
    for j in range(repeats):
        # Генерируем выборку из гамма-распределения
        sample = gamma.rvs(a=k, scale=theta, size=n)
        
        # Оцениваем параметр тета
        exp_theta = np.mean(sample) / k
        
        # Сохраняем абсолютную разность оценки от теоретического значения
        diff = np.abs(exp_theta - theta)
        if diff > 0.01:
            results[i, j] = 1

# Считаем количество раз, когда отклонение больше 0.01
counts = np.sum(results, axis=1)

# График
plt.plot(sizes, counts)
plt.xlabel('Размер выборки')
plt.ylabel('Количество отклонений ожидаемого тета от теоретического > 0.01')
plt.title('Гамма-распределение с параметрами thete=2 и k=3')
plt.show()