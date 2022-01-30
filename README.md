# HW3
Домашнее задания HW2.

Содержит 3 заданий.

Тема: NUnit

## Структура исходного кода
Каталог с решением имеет следующую структуру:
- **Tests.Common** - проект в котором хранится реализация http клиента, доменные объекты, ендпоинты, тест дата;
    - **TestServices.cs** - класс с DI в котором конфигурируется и инициализируется Http клиент; не требует доработок;
    - **HttpClientFactory.cs** - фабрика Http клиента; не требует доработок;
    - **HttpHelper.cs** - реализация отправки запросов на методы Post и Get; не требует доработок;
    - **TestServicesOptions.cs** - класс на который мапится конфигурация; не требует доработок;
    - **Endpoints.cs** - класс с ендпоинтами; не требует доработок;
    - **TestData.cs** - класс со всей тестовой датой для сохранения и прокидывания ее в тестовый метод через один класс; не требует доработок;
    - **TestServices.cs** - класс с DI в котором конфигурируется и инициализируется Http клиент; не требует доработок;
    - **CommentsResponce.cs** - класс десириализации респонса;
    - **TodosResponce.cs** - класс десириализации респонса;
    - **PostsRequest.cs** - класс сериализация реквеста;
- **Tests.Integration** - проект в котором хранится тесты и конфигурация;
    - **appsettings.json** - конфигурация. Не требует доработок;
    - **Task1.cs** - класс для реализации задания 1;
    - **Task2.cs** - класс для реализации задания 2;
    - **Task3.cs** - класс для реализации задания 3;


## Общее введение
Проверки которые надо написать на тесты могут падать, это ожидаемое поведение. 
При отправке запросов с новыми постами, объекты не будут создаваться на бекенде это тоже ожидаемо, ендпоинты куда идут запросы это просто моки. 
В проекте Common в папке Domain находятся, объекты для сериализации для отправки запросов и десериализации респонсов и дальнейшего ассерта по этим объектам. 
Помимо выполнения заданий будет оцениваться правильное именование тестовых методов через AAA паттерн, добавление правильных комментариев к ассертам и код стайл.  
#Так же нужно реализовать запуск всех тестов по заданию в 2 потока.

## Задание 1. Написать 2 теста проверяющие POST запрос.
Добавить эти тесты в класс Task1. 
Тесты будут проверять POST запрос на ендпоинт http://jsonplaceholder.typicode.com/posts. 
Метод их выполнения должен быnь общий, но разные данные должны прокидываться через IEnumerable в TestCaseSource.
Oбъект для отправки запроса уже создан в проект Сommon -> Configuration -> Domain -> PostsRequest. 


Для первого теста:
Оправить методом POST новый объект PostsRequest в котором Id и UserId будут рандомные уникальные числа, Body и  Title будут Guid + Test 1. 
Далее должна быть проверка, что респонс StatusCode равен Code 201 (Created)

Для второго теста:
Оправить методом POST новый объект PostsRequest существующий в котором Id и UserId равны 1, Body и Title будут Guid + Test 2.
Далее должна быть проверка, что респонс StatusCode равен Code 400 (BadRequest).
Тест будет падать на Assert, это нормально.

Объекты  создавать в IEnumerable, далее добавить их надо в TestData. В самом тесте уже из testData достать объект нужный для отправки запроса и ожидаемый респонс Error Code.

В задание надо переименовать тестовый метод с паттерном AAA, так же IEnumerable должен иметь валидное название для тестовой даты и написать читабельный и понятного еррор меседжа в ассерте. 


## Задание 2. Написать 2 теста проверяющие комментарии.
Добавить эти тесты в класс Task2. 
Тесты будут проверять GET запрос на ендпоинт https://jsonplaceholder.typicode.com/comments
Метод их выполнения должен быть общий, но разные данные должны прокидывается через IEnumerable в TestCaseSource.
Oбъект для десириализации респонса уже создан объект в проект Сommon -> Configuration -> Domain -> CommentsResponce. 

Для первого теста:
Нужно по-данному ендпоинту достать комментарий с айди 5, тоесть сделать GET запрос на "comments/5". 
Далее проверить, что что респонс StatusCode равен Code 200 (OK)
Далее десириализировать контент с респонса в объект CommentsResponce.
И сделать проверку по каждому полю. Все ассерты должны быть объеденины в Assert.Multiple. 
Полученный должен соответствовать данному.
{
  "postId": 1,
  "id": 5,
  "name": "vero eaque aliquid doloribus et culpa",
  "email": "Hayden@althea.biz",
  "body": "harum non quasi et ratione\ntempore iure ex voluptates in ratione\nharum architecto fugit inventore cupiditate\nvoluptates magni quo et"
}

Для второго теста: 
Нужно по-данному ендпоинту достать комментарий от пользователя с имейлом Lew@alysha.tv, тоесть сделать GET запрос на "?email=Lew@alysha.tv".
Далее проверить, что респонс StatusCode равен Code 200 (OK)
Далее десириализировать контент с респонса в объект CommentsResponce.
И сделать проверку по каждому полю. Все ассерты должны быть объеденины в Assert.Multiple. 
Полученный должен соответствовать данному.
{
  "postId": 1,
  "id": 1,
  "name": "alias odio sit",
  "email": "Lew@alysha.tv",
  "body": "non et atque\noccaecati deserunt quas accusantium unde odit nobis qui voluptatem\nquia voluptas consequuntur itaque dolor\net qui rerum deleniti ut occaecati"
}
Тест будет падать на Assert, это нормально.

## Задание 3. Написать тест проверяющий данные по ендпоинту todos.
Добавить эти тесты в класс Task3. 
Тесты будут проверять GET запрос на ендпоинт https://jsonplaceholder.typicode.com/todos
Метод их выполнения должен быть общий, но разные данные должны прокидыватся через IEnumerable в TestCaseSource.
Oбъект для десириализации респонса это List<TodosResponce>. 
Из всего респонса нужно достать только первых 3 елемента c коллекции. 
Ожидаемый объект для сравнения возвращает метод CreateExpectedTodosResponse.
Сравнивать объекты нужно через CollectionAssert.AreEqual. 
Так же должно быть проверка, что StatusCode в респонсе равен Code 200 (OK).
Тест должен проходить. 
