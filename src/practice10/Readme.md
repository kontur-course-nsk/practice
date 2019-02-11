### Текстовый редактор 2.0

Посмотрев на ваш текстовый редактор из практики 7, сын маминой подруги решил сделать свой. Ядро редактора он хочет сделать интерактивным консольным приложением: получает команды и аргументы из стандартного потока ввода и отправляет в поток вывода.

Ваш друг не успевает реализовать сам все команды и хочет добавить возможность его расширения снаружи (сейчас все команды реализованы в самом классе контроллера).

#### Задания 
- Изучить проект `ExtendableTextEditor`
- Используя свои знания про делегаты и лямбда выражения предложите и реализуйте механизм расширения текстового редактора новыми командами. При этом текущий контракт класса `EditController` должен остаться прежним, но можно добавлять дополнительные методы. Перенесите реализованные команды на использование этого механизма.
- Реализуйте команды из практики 7 для нового редактора. Логику работы можете взять из своей прошлой реализации (нужна только возможность применения, отмены операций тут нет) 

    Список команд:
    - backspace
    - delete
    - insert
    - movecursor
- При создании класса `EditController` можно указать его поведение при отсутствии запрашиваемой операции: возвращать неуспех или выбрасывать исключение. Предложите варианты реализации передачи настройки таким образом, чтобы она стала "горячей" (её можно было бы менять во время выполнения и это сразу бы влияло на поведение класса). Но помните, что `EditController` не должен напрямую зависеть от `GlobalEditorSettings`, потому что последний содержит настройки для всех модулей будущего текстового редактора. Реализуйте вариант с использованием делегатов.