'use strict';

//Модальное окно для кнопки Добавить
window.addEventListener('load', () => {
    const btnAdd = document.getElementById("Add");
    if (btnAdd) {
        btnAdd.addEventListener("click", () => {
            popup.classList.toggle('disable');
        });
    }
    const popup = document.querySelector(".popup");
    if (popup) {
        popup.addEventListener("click", (event) => {
            if (event.target === popup) popup.classList.toggle('disable');
        });
    }
    // Мы здесь получаем элементы по id и меняем класс disable как вкл-выкл у popup.
    // При нажатии Добавить-открываем окно, при нажатии вне окна(серое место) закрываем его


    let list = document.querySelector(".list__wrapper");// получаем <div class="list__wrapper shadow">
    const popup_btn = document.getElementById("popup-btn");
    if (popup_btn) {
        popup_btn.onclick = () => { //при клике на Отправить мы делаем...
            var listLeng = document.getElementsByClassName('list__lines').length;// чтобы получить сколько есть записей
            let text = Array.from(popup.getElementsByTagName('input'));
            let divList = document.createElement('div');
            divList.classList.add('list__lines');
            let number = document.createElement('p');
            number.classList.add('number');
            number.textContent = listLeng + 1; // наш номер будет равен = количество записей на дан. момент + 1

            /* дальше мы просто реализуем вот такую структуру
            <div class="list__line-title">
                <p class="number">1</p>
                <p class="name">текст</p>
                <p class="chip">текст</p>
                <p class="nick">текст</p>
                <div class="list__param">
                    <div class="list__more">Изменить</div>
                    <div class="list__remove">Удалить</div>
                </div>
            </div>
            но в js переменных 
            */
            let name = document.createElement('p');
            name.classList.add('name');
            let chip = document.createElement('p');
            chip.classList.add('chip');
            let nick = document.createElement('p');
            nick.classList.add('nick');
            let param = document.createElement('div');
            let change = document.createElement('div');
            let remove = document.createElement('div');
            param.classList.add('list__param');
            param.appendChild(change);
            param.appendChild(remove);
            change.classList.add('list__more');
            remove.classList.add('list__remove');
            change.textContent = 'Подробней';
            remove.textContent = 'Удалить';

            divList.appendChild(number);
            divList.appendChild(name);
            divList.appendChild(chip);
            divList.appendChild(nick);
            divList.appendChild(param);

            let i = 1;
            text.forEach(element => {
                const a = divList.querySelectorAll('p');
                a[i].textContent = element.value;
                i++;
            });
            // то что мы писали в модальном окне мы берём оттуда и закидываем в нашу структуру в Js
            list.appendChild(divList);// нашу структуру с данными кидаем в конец <div class="list__wrapper shadow">
            popup.click();// закрываем окно
        };
    }


    const checkbox = document.getElementById("checkbox")
    checkbox.addEventListener("change", () => {
        const l = document.querySelector('.list');
        const mTitle = document.querySelector('.main__title');
        const m = document.querySelector('.main');
        if (popup) {
            const p = popup.querySelector(".popup__box");
            p?.classList.toggle('dark-popup');
        }
        l?.classList.toggle('list-dark');
        list?.classList.toggle('shadow');
        document.body.classList.toggle("dark");
        m?.classList.toggle('shadow');
        mTitle?.classList.toggle('dark_white');
    })


    // кнопка изменения
    const btn_change = document.querySelector("#newChange");
    btn_change.addEventListener("click", () => {
        const item_input = document.querySelectorAll('.main__column2 .main__elem-input');
        const item_elem = document.querySelectorAll('.main__column2 .main__elem');
        const btn_save = document.querySelector('#saveButton');
        btn_save.classList.toggle('disable');
        item_input.forEach((input, index) => {

            input.classList.toggle('disable');
            item_elem[index].classList.toggle('disable');

            const textContent = item_elem[index].textContent;

            input.value = textContent;
        });

    })



});
function submitForm() {
    console.log('submitForm called'); // Проверка, вызывается ли функция
    const searchQuery = document.getElementById('search').value;
    const hiddenInput = document.getElementById('hiddenSearch');

    // Устанавливаем значение в скрытое поле
    hiddenInput.value = searchQuery;

    // Отправляем форму
    document.getElementById('searchForm').submit();
}
