# coding=utf-8
import numpy as np

# Начальные параметры
mu1 = 2
mu2 = 1
sigma1 = 1
sigma2 = 0.5
alpha = 0.05

repeats = 1000

# Высчитываем доверительный интервал
def confidence_interval(sample_X, sample_Y, sigma):
    X_avg = np.mean(sample_X)
    Y_avg = np.mean(sample_Y)
    tau = X_avg - Y_avg
    lower = tau - 1.96*sigma
    upper = tau + 1.96*sigma
    return (lower, upper)

# Генерируем две выборки размера n и m и возвращаем кол-во раз, когда tau было покрыто доверительным интервалом
def gen_sample(n, m):
    cnt = 0
    sigma = np.sqrt(sigma1/n + sigma2/m)
    for _ in range(repeats):
        sample_X = np.random.normal(loc=mu1, scale=np.sqrt(sigma1), size=n)
        sample_Y = np.random.normal(loc=mu2, scale=np.sqrt(sigma2), size=m)
        ci = confidence_interval(sample_X, sample_Y, sigma)
        if ci[0] <= mu1 - mu2 <= ci[1]:
            cnt += 1
    return cnt

# Запуск функции и вывод результата
print("Percentage of coverage for a sample of size 25:", gen_sample(25, 25)/repeats)
print("Percentage of coverage for a sample of size 10000:", gen_sample(10000, 10000)/repeats)