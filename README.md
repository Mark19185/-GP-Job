# -GP-Job
Граббер постов с новостных источников для дальнейшей загрузки в мобильное приложение

Парсит статьи с указанных сервисов по заданному интервалу путём разбора html структуры страницы и загружает их в БД.
Используется либа htmlAgilityPack с надстройкой Fizzler, можно заюзать selenium, но этот вариант оптимальнее.
Код заточен под расширение сервисов, достаточно прочекать структуру интересующего сайта и кинуть указатели на необходимые блкоки

