## Практика к блоку "Многопоточное программирование"

На этом практическом занятии вам предстоит реализовать несколько схем обработки заданий.

В проекте уже есть классы `UserJobProvider` и `AdminJobProvider`, которые поставляют задания.
Также есть класс `Handler`, который обрабатывает задания.
Вам, лишь, нужно написать код, который будет брать задания из поставщика и отдавать обработчику.
Но сделать это нужно будет несколько раз, каждый раз в новом файле с новыми условиями.

Тестов нет. Можно протестировать схему, запустив её в классе `Program`.
После реализации каждой схемы нужно позвать преподавателя, чтобы он проверил.

### OneProviderOneWorker

В этой схеме задачи нужно обрабатывать в один поток.
Однако, перед тем как получить задания из поставщика нужно его запустить в отдельном потоке.

Программа должна корректно завершаться по сочетанию клавишь Ctrl+C.
Код, который отслеживает это сочетание клавиш уже есть в `Program`.
Нужно только корректно обрабатывать состояние `CancelationToken`.

### OneProviderOneWorkerWithErrors

Эта схема очень похожа на предыдущую. Однако, при обработке одного из заданий будет выкинуто исключение.
При этом всё приложение должно прекратить свою работу с ненулевым кодом.

### OneProviderManyWorkers

В этой схеме нужно обрабатывать задания в несколько потоков.

### TwoProvidersManyWorkers

В этой схеме у нас появляется второй поставщик заданий.
Нужно стараться брать задания в том порядке, в котором они приходят, и распределять их по потокам обработки.
При этом потоки обработки не должны простаивать если заданий какого-либо типа не хватает.

### OneProviderManyWorkersWithLockByUserId

Эта схема похожа на `OneProviderManyWorkers`. Но одновременно может выполняться только одно задание с одинаковым `UserId`.
При этом обработка не должна останавливаться если пришло несколько заданий с одним UserId подряд.

Пример: в обработке userId=1, в очереди userId=1, userId=1 и только на третьем месте userId=2.
Свободный поток обработки должен пропустить первые два задания и взять третье.