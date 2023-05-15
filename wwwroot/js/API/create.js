loadCategories();

function creaPost() {

    /*if (document.getElementById('category').value == '') {
        postToCreate = {
            title: document.getElementById('title').value,
            description: document.getElementById('description').value,
            categoryid: null,
            image: document.getElementById('image').value
        };
    } else {
        postToCreate = {
            title: document.getElementById('title').value,
            description: document.getElementById('description').value,
            categoryid: document.getElementById('category').value,
            image: document.getElementById('image').value
        };
    }*/

    postToCreate = {
        title: document.getElementById('title').value,
        description: document.getElementById('description').value,
        categoryid: document.getElementById('category').value == '' ? null : document.getElementById('category').value,
        image: document.getElementById('image').value == '' ? null : document.getElementById('image').value
    };
    

    axios.post('/api/PostsAPI', postToCreate)
        .then((res) => {
            alert('post creato correttamente');
            window.location.href = '/Client';
        })
        .catch((res) => {
            //gli errori sono in questo dictionary: res.response.data.errors
            for (let errorKey in res.response.data.errors) {
                // testo errore: res.response.data.errors[errorKey]
                let spanId = errorKey.toLowerCase() + "_validation";
                let span = document.getElementById(spanId);
                span.innerHTML = res.response.data.errors[errorKey];
                console.log('Errore: ' + res.response.data.errors[errorKey]);
            }
            console.error("errore", res);
        });
}


function loadCategories() {
    axios.get('/api/CategoriesAPI')
        .then((res) => {        //se la richiesta va a buon fine
            console.log('risposta ok', res);
            //svuoto la tabella
            document.getElementById('category').innerHTML = '<option value=""></option>';

            res.data.forEach(category => {
                document.getElementById('category').innerHTML +=
                    `<option value="${category.id}">${category.name}</option>`;
            })
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });
}

