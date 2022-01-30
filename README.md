# HW3

Для выполнения заданий надо:
1. В текущий проект добавить модуль который будет отвечать за отправку http запросов (можно самостоятельно реализовать с System.Net.Http или можно сторонюю библиотеку использовать). 
2. Добавить пакет Newftosoft для сериализиции обьектов которые будут отправлятся в запросах и десириализации обьектов которые будут приходит в респонсе.
3. Проверки которые надо написать на тесты могут падать, это ожидаемое поведение. 
4. При отправке запросов с новыми постами, обьекы не будут создаваться на бекенде это тоже ожидаемо, данные ендпоинты просто моки.  

# Задание: 1 http://jsonplaceholder.typicode.com/posts
Написать тесты которые будут проверять POST запрос на данный ендпоинт с объектом поста (Обьект { "userId": 1, "id": 1, "title": text, "body": text }). 
Примеры объекта можно получить по данному ендпоинту с GET запросом. Сам объект должен быть создан в моделях. 
Должны быть как позитивные сценарии так и негативные с проверкой на error code.

# Задание: 2 http://jsonplaceholder.typicode.com/posts
Написать тесты которые будут проверять PATCH запрос на данный ендпоинт. 
Нужно создать пост через отправку POST запроса. 
Далее для этого поста сделать PATCH по полю "title" это первый тест и по полю "body".
Должны быть проверка на error code.

# Задание: 3 http://jsonplaceholder.typicode.com/posts
Написать тесты которые будут проверять DELETE запрос на данный ендпоинт. 
Нужно создать пост через отправку POST запроса. 
Далее удалить этот пост через DELETE запрос. 
Должны быть проверка на error code.

# Задание: 4 https://jsonplaceholder.typicode.com/comments
Проверить что имейл который оставил коментарий с текстом в body "ipsum dolorem" пренадлежит "Marcia@name.biz". 
Для этого надо создать обьект коментарий { "postId": 1, "id": 1, "name": "id labore", "email": "Eliseo@gardner.biz", "body": "laudantium" }. 
Отправить запрос GET на данный ендпоинт, получить все коментарии. 
Далее сделать проверку, что  коментарий с текстом в body "ipsum dolorem" пренадлежит "Marcia@name.biz" 

# Задание: 5 https://jsonplaceholder.typicode.com/posts
Проверить, что юзер кто сделал пост с title "eos dolorem iste accusantium est eaque quam" имя "Patricia Lebsack"
Для этого надо отправить запрос GET на ендпоинт (https://jsonplaceholder.typicode.com/comments), получить все коментарии. 
В коментариях находим id поста с title "eos dolorem iste accusantium est eaque quam".
Далее оправить GET запрос на ендпоинт (https://jsonplaceholder.typicode.com/posts) получить все посты.
По айди комментария из коллекции постов достаем имя. 
Делаем проверку что имя совпадает с ожидаемым.

# Задание: 6 https://jsonplaceholder.typicode.com/photos
Проверить, что фото с title "ad et natus qui" пренадлежит юзеру с email "Noemie@marques.me"
Для этого надо создать обьект фото { "albumId": 1,"id": 18, "title": "laboriosam", "url": "https://via/6", "thumbnailUrl": "https://v/1" }. 
Далее надо отправить запрос GET на ендпоинт (https://jsonplaceholder.typicode.com/photos), получить все фото. 
Найди айди поста с title "ad et natus qui".
Далее оправить GET запрос на ендпоинт (https://jsonplaceholder.typicode.com/posts) получить все посты.
По айди с фото из коллекции постов достаем email. 
Делаем проверку что email совпадает с ожидаемым.
