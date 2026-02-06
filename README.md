# Запуск

В терминале:

```
git clone https://github.com/WebCoder1980/wizardsoft
cd wizardsoft
```

Дальнейшие шаги зависят от вариантов

Стандартные пользователи:

```
admin | admin_password (это админ)
maxsmg | qweqwe (это пользователь)
```

# Скриншоты работы

Приложение содержит CRUD для работы с деревом и авторизация+регистрация для пользователей. 

<img width="1131" height="896" alt="image" src="https://github.com/user-attachments/assets/e6818e30-6548-4b6c-9d90-3cab75154deb" />


Ниже привидены примеры работы:

## Безопастность

## Без авторизации операции над иерархической моделью недоступны

<img width="1448" height="684" alt="image" src="https://github.com/user-attachments/assets/35db3379-eb83-4877-a8d0-3175de5ebd3c" />

## Авторизация

<img width="1146" height="882" alt="image" src="https://github.com/user-attachments/assets/06093ff4-366c-4502-b619-305f25c13733" />

<img width="1085" height="901" alt="image" src="https://github.com/user-attachments/assets/97fc82ed-1700-49ec-bf00-844e954122e6" />

<img width="1164" height="879" alt="image" src="https://github.com/user-attachments/assets/eedc4982-506a-4ad3-909f-a2ada20c7240" />

## Авторизовавшись под пользователем можно смотреть, но не редактировать

<img width="1150" height="790" alt="image" src="https://github.com/user-attachments/assets/03bd0136-7097-4e31-bf67-febfe9427c0e" />

<img width="1138" height="900" alt="image" src="https://github.com/user-attachments/assets/b1536d49-128a-4d07-88f7-4ead386696cd" />

## Админ может редактировать

<img width="972" height="919" alt="image" src="https://github.com/user-attachments/assets/9a206c52-87d8-4412-a8b8-6994127af3ce" />


## Рекурсивное удаление веток

Было:

<img width="1302" height="730" alt="image" src="https://github.com/user-attachments/assets/26e82982-e12d-4172-9d30-a41c610a14f1" />

Стало:

<img width="1290" height="678" alt="image" src="https://github.com/user-attachments/assets/d9401016-3e85-499f-8b6e-87d19ad37ca6" />

<img width="1290" height="908" alt="image" src="https://github.com/user-attachments/assets/c0a272f7-4074-415b-a0be-f3c1d762e1ff" />

## Обнаружение цикла при изменении дерева

<img width="1064" height="925" alt="image" src="https://github.com/user-attachments/assets/dcdc3f44-4bfb-43de-8d9f-8b25bdac0719" />

## Через Docker

```
docker build -t wizardsoft_testtask .

docker run -p 35001:8080 -e ASPNETCORE_ENVIRONMENT=Development wizardsoft_testtask
```

Адрес к Swagger: http://localhost:35001/swagger/index.html

## В Visual Studio

Открыть проект.

Нажать F5.

Откроется новое окно браузера со `Swagger`.

# Описание

Создать минималистичный веб-сервис с использованием ASP.NET Core (.NET8.0), EF Core, SQLite, Docker.
Сервис должен содержать:
- древовидную иерархическая модель данных без ограничения уровня вложенности (структура каталогов).
- минимальные Rest API покрывающие CRUD операции над этой моделью.
- добавить поддержку транзакций при изменении иерархической структуры.
- запретить создание циклических ссылок в иерархии.
- аутентификация JWT.
- авторизация по ролям (только администратор может удалять и изменять узлы).
- возможность экспорта дерева в формате JSON.
Демонстрация работы приложения через Swagger UI.
Уделить внимание чистому стилю кода и общей завершенности приложения.
