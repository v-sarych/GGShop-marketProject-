function createTag () {
    let tagName = document.querySelector(".cta_section__tag_input").value;

    const request = new XMLHttpRequest();

    request.open('POST', `${host}/api/Product/Tag/Create`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const tagID = JSON.parse(request.responseText)
            getAllTags()
            document.querySelector(".cta_section__tag_input").value = "";
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(tagName))
}

function getAllTags () {
    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/Product/Tag/GetAll`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const tags = JSON.parse(request.responseText);
            console.log(tags)
            displayAllTags(tags)
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })

    request.send()
}

function displayAllTags (tags) {
    const tableBody = document.querySelector(".table__body_data--tags");
    let dataHTML = '';
    for (let tag of tags) {
        dataHTML += `<tr class="table__body_row" id="table__body_row__${tag.id}">
                        <td class="table__cell">${tag.id}</td>
                        <td class="table__cell" id="cell_name__${tag.id}">${tag.name}</td>
                        <td><i class="fa-solid fa-trash input__tag_delete_button" id="input__delete_button__${tag.id}" onclick="deleteTag(${tag.id})"></i></td>
                    </tr>`;
    }
    tableBody.innerHTML = dataHTML;
}

function deleteTag(id) {
    const request = new XMLHttpRequest();

    request.open('DELETE', `${host}/api/Product/Tag`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            var row = document.getElementById('table__body_row__' + id);
            var parent = row.parentNode;
            parent.removeChild(row);
            showPopUp("error", "Вы удалили тег!")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(id))
}

window.addEventListener('tagsSectionLoaded', () => {
    document.querySelector(".cta_section__tag_input").addEventListener("keydown", function(event) {
        if (event.key === "Enter" && document.querySelector(".cta_section__tag_input").value.trim() !== "") {
            createTag ()
            document.querySelector(".cta_section__tag_input").value = '';
        }
    });
    getAllTags()
});

function filterTags() {
    const tagName = document.getElementById("search_input__tag_name").value;

    let filteredTags = tagData;

    if (tagName && tagName.trim() !== "") {
        filteredTags = filteredTags.filter(tag => tag.id && tag.name.includes(tagName));
    }

    loadTagsData(filteredTags);
}