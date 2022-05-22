**Project 2**

_ВВОДНЫЕ ДАННЫЕ_

Во втором проекте необходимо сделать тестовый проект, состоящий из набора автоматизированных тестов, для тестирования REST-сервиса. В качестве объекта тестирования используется сервис **https://gorest.co.in** который моделирует источник данных для блога.

Сервис предоставляет 4 ресурса в 2-х версиях _(v1 – для группы 1 и 3 и v2 – для групып 2 и 4)_:

1. Список пользователей - _https://gorest.co.in/public/v1/users_ и _https://gorest.co.in/public/v2/users_

2. Посты, которые написали пользователи - _https://gorest.co.in/public/v1/posts_ и _https://gorest.co.in/public/v2/posts_

3. Комментарии к постам - _https://gorest.co.in/public/v1/comments_ и _https://gorest.co.in/public/v2/comments_

Список дел каждого пользователя - _https://gorest.co.in/public/v1/todos_ и _https://gorest.co.in/public/v2/todos_ (не тестируем)



Каждый из ресурсов реализован в 2-х версиях, где одинаковые данные представлены в разных форматах. Все ресурсы реализовывают HTTP методы **GET** (получение данных), **POST** (создание новой сущности), **PUT** (замена существующей сущности), **PATCH** (частичное изменение сущности), **DELETE** (удаление).

3 ресурса (кроме ресурса с пользователями) поддерживают представление данных в виде json и xml. Для того чтобы получить представление в формате xml, Нужно к адресу ресурса добавить окончание .xml, например:

_https://gorest.co.in/public/v2/posts.xml_, аналогично, чтобы в явном виде запросить ответ в формате json, необходимо добавить окончание .json, например:

_https://gorest.co.in/public/v2/posts.json_. По умолчанию, без указания окончания .json или .xml данные будут возвращаться в формате json.

Сервис также предоставляет вложенные ресурсы, к примеру список постов, которые сделал пользователь с id = 3156, можно получить из такого ресурса: _https://gorest.co.in/public/v2/users/3156/posts_



_ЗАДАНИЕ_

Протестировать все 4 эндпоинта сервиса по ряду тест кейсов.

Требование к тест кейсам:

1. Весь функционал сервиса необходимо проверить на то, что он работает и возвращает правильные данные и корректный response code при вводе корректных данных (happy path scenario). Тесты провести для всех методов **GET, POST, PUT, PATCH, DELETE.**

2. Проверить работу пагинации. При запросе конкретной страницы, нужно убедиться, что сервис возвращает именно запрошенную страницу. Для ресурсов v1 пагинацию проверять по данным в теле ответа, для v2 - проверять по заголовкам, которые приходят в ответе.

3. Реализовать тесты для негативных кейсов

   a. запрос несуществующей сущности из каждого ресурса

   b. запрос на создание\модификацию\удаление без токена авторизации

   c. создание сущности, которая уже существует

   d. создание сущности из невалидного json

4. Сложные сценарии тестов - придумать и реализовать тестовые кейсы для вложенных ресурсов: https://gorest.co.in/public/v2/users/<userId>/posts и https://gorest.co.in/public/v2/posts/<postId>/comments

_ФУНКЦИОНАЛ_

Одним из вариантов запуска тестов есть консольная команда _dotnet test_. Откройте PowerShell в папке с .sln файлом проекта и выполните эту команду.
Существует несколько категорий тестов [Post, Get, Put, Patch, Delete, Difficult, ...]. Чтобы запустить тесты конкретной категории выполните команду _dotnet test --filter TestCategory=ConcreteCategory__, _ConcreteCategory_ желанная категория тестов.


Результаты тестов сохраняются в файле _TestResultsDateTime.json по относительному пути ..\Tests.Integration\bin\Debug\net6.0

