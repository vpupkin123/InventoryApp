  Inventory Web Client — README \* { margin: 0; padding: 0; box-sizing: border-box; } body { font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Arial, sans-serif; line-height: 1.7; color: #333; background: #f5f7fa; } .header { background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); color: white; padding: 50px 20px; text-align: center; } .header h1 { font-size: 3em; margin-bottom: 15px; } .header p { font-size: 1.3em; opacity: 0.95; max-width: 700px; margin: 0 auto; } .container { max-width: 900px; margin: 0 auto; padding: 40px 20px; } .section { background: white; padding: 35px; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.05); margin-bottom: 30px; } h2 { color: #f5576c; border-bottom: 2px solid #f5576c; padding-bottom: 10px; margin-bottom: 20px; font-size: 1.8em; } h3 { color: #f093fb; margin: 20px 0 12px 0; font-size: 1.3em; } h4 { color: #333; margin: 15px 0 8px 0; font-size: 1.1em; } p { margin-bottom: 12px; } ul { margin-left: 25px; margin-bottom: 15px; } li { margin-bottom: 8px; } .download-card { background: #f8f9fa; padding: 30px; border-radius: 6px; border-left: 4px solid #f5576c; text-align: center; margin: 20px 0; } .download-card h3 { color: #f5576c; margin: 10px 0; font-size: 1.5em; } .download-card .platform-icon { font-size: 4em; margin-bottom: 10px; } .download-btn { display: inline-block; background: #f5576c; color: white; padding: 12px 30px; border-radius: 5px; text-decoration: none; margin-top: 15px; font-weight: bold; font-size: 1.1em; transition: background 0.3s; } .download-btn:hover { background: #e04558; } .steps { background: #f8f9fa; padding: 25px; border-radius: 6px; margin: 15px 0; } .step { display: flex; align-items: flex-start; margin-bottom: 15px; } .step:last-child { margin-bottom: 0; } .step-number { display: inline-block; background: #f5576c; color: white; width: 30px; height: 30px; line-height: 30px; text-align: center; border-radius: 50%; margin-right: 15px; font-weight: bold; flex-shrink: 0; } .step-content { flex: 1; } .note { background: #e3f2fd; border-left: 4px solid #2196f3; padding: 15px; margin: 15px 0; border-radius: 4px; } .warning { background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 15px 0; border-radius: 4px; } code { background: #f4f4f4; padding: 2px 6px; border-radius: 3px; font-family: "Consolas", monospace; color: #d63384; } .footer { text-align: center; padding: 30px; color: #666; font-size: 0.95em; } .footer a { color: #f5576c; text-decoration: none; } .footer a:hover { text-decoration: underline; } @media print { .header { background: #f5576c !important; } .section { box-shadow: none; border: 1px solid #ddd; } }

# 💻 Inventory Web Client

Десктопное приложение для сбора информации об оборудовании и генерации JSON-отчётов для системы Inventory Web.

## О приложении

**Inventory Web Client** — это лёгкое десктопное приложение, которое автоматически собирает подробную информацию об оборудовании вашего компьютера, операционной системе и сетевой конфигурации. Оно генерирует файл отчёта в формате JSON, который можно загрузить в систему Inventory Web для учёта ИТ-активов.

### Что собирает приложение:

*   Производитель и модель компьютера
*   Информация о материнской плате и серийный номер
*   Детали процессора (модель, ядра, потоки, тактовая частота)
*   Модули оперативной памяти (ёмкость, скорость, тип)
*   Устройства хранения (исключая USB и съёмные накопители)
*   Версия и сборка операционной системы
*   Сетевая конфигурация (IP-адрес, имя компьютера)

**💡 Конфиденциальность:** Приложение собирает только информацию об оборудовании и системе. Личные файлы, документы или конфиденциальные данные не считываются и не передаются.

## Скачать

Выберите версию для вашей операционной системы:

🪟

### Windows

Windows 7 или выше

[📥 Скачать v1.0.0.zip](https://github.com/vpupkin123/InventoryApp/releases/download/v1.0.0/v1.0.0.zip)

## Как использовать

### Windows

1

Скачайте **v1.0.0.zip** из раздела выше и распакуйте архив

2

Запустите файл `InventoryApp.exe` двойным кликом (установка не требуется)

3

Приложение автоматически соберёт информацию об оборудовании

4

В той же папке будет создан JSON-файл отчёта (имя вида `report_*.json`)

5

Загрузите этот файл через интерфейс Inventory Web

## Системные требования

### Windows

*   Windows 7, 8, 10 или 11
*   Дополнительное программное обеспечение не требуется
*   Права администратора не обязательны (но рекомендуются для полного сбора данных)

## Формат вывода

Приложение генерирует JSON-файл со следующей структурой:

*   **Информация о пользователе:** ФИО, имя файла, временная метка
*   **Системная информация:** имя компьютера, версия ОС, IP-адрес
*   **Информация об оборудовании:** полные данные о материнской плате, процессоре, оперативной памяти и устройствах хранения

**💡 Именование файлов:** Отчёты автоматически именуются по шаблону `report_[временная_метка].json`, чтобы избежать перезаписи предыдущих отчётов.

## Поддержка

Если у вас возникли проблемы или вопросы:

*   Проверьте основную документацию проекта: [Руководство пользователя](../docs/ru_user-guide.html)
*   Создайте Issue в основном репозитории: [github.com/vpupkin123/inventory-web](https://github.com/vpupkin123/inventory-web)
*   Свяжитесь с разработчиком: [vpupkin123](https://github.com/vpupkin123)

**Inventory Web Client** — часть проекта [Inventory Web](https://github.com/vpupkin123/inventory-web)

Разработано [vpupkin123](https://github.com/vpupkin123)