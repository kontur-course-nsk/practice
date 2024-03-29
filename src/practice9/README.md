Используя уже известные вам структуры данных, разработайте новую структуру данных для [кеша](https://ru.wikipedia.org/wiki/Кэш).

Добавление и получение элемента нужно реализовать в виде индексатора.
Добавление элемента по существующему ключу перезаписывает значение.
Получение элемента по несуществующему ключу приводит к `KeyNotFoundException`.
Метод `RemoveLeastRecentlyUsed` удаляет элемент, который дольше всего не используется.
Под использованием имеется ввиду добавление и чтение.
Все методы должны работать за константу.

В качестве усложнения можно добавить в кеш поле `Capacity`.
Если добавление элемента приводит к тому что количество элементов становится больше `Capacity`,
нужно автоматически удалить элемент, который дольше всего не используется.
Для новой функциональности нужно написать тесты.
