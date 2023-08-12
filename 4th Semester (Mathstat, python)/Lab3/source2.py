#coding=utf-8
import numpy as np
from scipy.stats import norm

# Ќачальные параметры
alpha = 0.05
theta = 5

repeats = 1000

# √енерируем выборку, находим доверительный интервал пор€дка 1-alpha, считаем кол-во попаданий параметра в полученный интервал
def gen_sample(n):
    cnt = 0
    for _ in range(repeats):
        sample = np.random.uniform(-theta, theta, n)

        q = norm.ppf(1 - alpha/2)
        mean_of_edges = (np.sort(sample)[0]+np.sort(sample)[-1])/2
        lower_bound = mean_of_edges - (np.std(sample)) * q / np.sqrt(n)
        upper_bound = mean_of_edges + (np.std(sample)) * q / np.sqrt(n)
    
        if lower_bound <= np.mean(sample) <= upper_bound:
            cnt += 1
    return cnt

# ¬ыводим результаты эксперимента
print("Percentage of coverage for a sample of size 25: ", (gen_sample(25)/repeats))
print("Percentage of coverage for a sample of size 10000: ", (gen_sample(10000)/repeats))