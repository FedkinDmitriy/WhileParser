
## Описание проекта  
Программа представляет собой WinForms-приложение для анализа строкового выражения с циклом `while`. Она использует регулярные выражения для проверки синтаксической корректности ввода и хеш-таблицу для хранения идентификаторов и их описаний.  

## Функциональность  
- Проверка синтаксиса выражения `while(...) { ... }` с использованием регулярных выражений.  
- Определение переменных, констант и терминалов в выражении.  
- Хранение идентификаторов в хеш-таблице с линейным пробированием.  
- Поиск элементов в таблице по индексу.  
- Отображение списка найденных идентификаторов.  

## Используемые технологии  
- **Язык**: C#  
- **Платформа**: .NET (WinForms)  
- **Структуры данных**:  
  - Регулярные выражения (`Regex`)  
  - Список (`List<T>`)  
  - Хеш-таблица (реализована через массив `Token[]` с линейным пробированием)  

## Основные компоненты кода  
### `Form1.cs` (основная форма)  
- `patternIsWhile` — регулярное выражение для проверки корректности синтаксиса `while(...) { ... }`.  
- `buttonCheck_Click` — обработчик кнопки проверки строки, который:  
  - Анализирует строку.  
  - Заполняет хеш-таблицу идентификаторами.  
  - Отображает результаты в `ListView`.  
- `button1_Click` — поиск элемента по индексу в хеш-таблице.  
- `buttonList_Click` — вывод найденных элементов в `MessageBox`.  

### `Token.cs` (модель идентификатора)  
Класс `Token` представляет идентификатор с полями:  
- `Name` — имя переменной/константы.  
- `Description` — описание (например, "целочисленная переменная" или "константа символов").  

## Запуск проекта  
1. Открыть решение в **Visual Studio**.  
2. Запустить **WhileParser**.  
3. Ввести строку в `textBoxUserString`.  
4. Нажать **"Проверить"**.  
5. При корректном вводе строка подсветится зеленым, а найденные элементы отобразятся в `ListView`.  

## Пример ввода  
while(as22+ee4:=true){Xs22+e44;}
После анализа хеш-таблица будет содержать:  
| №  | Имя   | Описание |  
|----|-------|------------|  
| 1  | as22  | целочисленная переменная |  
| 2  | ee4   | целочисленная переменная |  
| 3  | true  | константа символов |  
| 4  | Xs22  | целочисленная переменная |  
| 5  | e44   | целочисленная переменная |  

## Автор  
Разработано для лабораторной работы по анализу выражений в C# WinForms.  


