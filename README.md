# RotationTask

Дан одномерный массив ``int[N]``, который случайно заполнен степенями двойки (``2, 4, 8, 16 и т.д.``). Над массивом можно выполнить две операции: сдвиг влево и вправо. При сдвиге влево (вправо) два рядом стоящих одинаковых числа суммируются и новое значение помещается в левую (правую) ячейку. Значения в массиве которые находились правее (левее) сдвигаются на одну позицию влево (вправо). Смещение применяется на весь массив например ``4, 4, 2, 4, 4`` при смещении в любую из сторон даст результат ``8, 2, 8``. Написать алгоритм для того, чтобы найти последовательность сдвигов, которая приведет к минимальной длине массива. 

# Solutions

## 1: Vectored
  1.1) Решение без доп. классов и структур. Основано на бинарной логике и веторах из ``System.Numerics``. Перед исполнением основного  теста выводит количество одновременно обрабатываемых чисел. Для процессоров поддерживающих AVX это значение будет ``4``, для процессоров поддерживающих AVX2 это значение будет ``8``, и для процессоров поддерживающих AVX-512 это значение будет ``16``.
  Из плюсов, показывает очень высокую скорость при отсутствии нечётного количества одинаковых цифр подряд.
  Из недостатков, дублирование кода в методах tryL и tryR, вызовы Array.CopyTo(...)
  
