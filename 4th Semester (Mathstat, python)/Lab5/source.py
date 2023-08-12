#coding=utf-8
import pandas as pd
import numpy as np
import statsmodels.api as sm
from scipy import stats

data = pd.read_csv('song_data.csv')

X = data[['song_duration_ms', 'danceability', 'energy']]
y = data['song_popularity']

# Свободный коэффициент
X = sm.add_constant(X)

# Коэффициенты зависимости модели
coefficients = np.linalg.inv(X.T.dot(X)).dot(X.T).dot(y)
coefficients = np.array([round(num, 3) for num in coefficients])

# Остаточная дисперсия
y_predicted = X.dot(coefficients)
deviation = y - y_predicted
residual_var = np.sum(deviation**2) / (len(X) - 2)

# Доверительные интервалы
alpha = 0.05
cov_matrix = residual_var * np.linalg.inv(X.T.dot(X))
std_errs = np.sqrt(np.diag(cov_matrix))
t_values = stats.t.ppf(1-alpha/2, len(data) - len(coefficients))
confidence_intervals = np.column_stack((coefficients - t_values * std_errs, coefficients + t_values * std_errs))

# Коэффициент детерминации
tss = np.sum((y - np.mean(y))**2)
ess = np.sum((y_predicted - np.mean(y))**2)
r_squared = ess / tss

# Стандартные ошибки оценки коэффициентов
se = np.sqrt( np.diagonal( residual_var * np.linalg.inv(X.T.dot(X)) ) )

# Считаем доверительные интервалы для 0.95
alpha = 0.05
t = np.abs(stats.t.ppf(alpha / 2, df=X.shape[0] - X.shape[1]))
intervals = []
for i in range(coefficients.shape[0]):
    lower_bound = coefficients[i] - t * se[i]
    upper_bound = coefficients[i] + t * se[i]
    intervals.append((round(lower_bound, 3), round(upper_bound, 3)))

# Проверяем гипотезы и выводим результаты

# H0 - Чем больше энергичность, тем больше популярность
_, p_value_energy = stats.ttest_ind(data['song_popularity'], data['energy'])
print("Popularity - energy:", round(p_value_energy, 3))
if p_value_energy < 0.05:
    print("H0 is rejected. Popularity does not depend on energy")
else:
    print("H0 is accepted. Popularity depends on energy")
print()



# H0 - Популярность зависит от продолжительности
_, p_value_duration = stats.ttest_ind(data['song_popularity'], data['song_duration_ms'])
print("Popularity - duration:", round(p_value_duration, 3))
if p_value_duration < 0.05:
    print("H0 is rejected. Popularity does not depend on duration")
else:
    print("H0 is accepted. Popularity depends on the duration")
print()



# H0 - Коэффициенты при энергичности и танцевальности одноверменно равны 0
_, p_value_energy = stats.f_oneway(data['song_popularity'], data['energy'])
_, p_value_danceability = stats.f_oneway(data['song_popularity'], data['danceability'])
print("Popularity - energy:", round(p_value_energy, 3))
print("Popularity - danceability:", round(p_value_danceability, 3))
if p_value_energy > 0.05 or p_value_danceability > 0.05:
    print("H0 is rejected. The coefficients for energy and danceability are not equal to 0 at the same time")
else:
    print("H0 is accepted. The coefficients for energy and danceability are equal to 0 at the same time")
print()




print()

print("Coefficients")
print("Free coef:", coefficients[0])
print("Duration coef:", coefficients[1])
print("Danceability coef:", coefficients[2])
print("Energy coef:", coefficients[3])

print()

print("Confidence intervals")
print("Free coef:", intervals[0])
print("Duration interval:", intervals[1])
print("Danceability interval:", intervals[2])
print("Energy interval:", intervals[3])

print()

print("Models params")
print("Residual variance:", round(residual_var, 3))
print("R^2:", round(r_squared, 3))

print()
print()
print()
print()
print()
print()

# Для сравнения с "идеальной" моделью
model = sm.OLS(y, X)
results = model.fit()
print(results.summary())