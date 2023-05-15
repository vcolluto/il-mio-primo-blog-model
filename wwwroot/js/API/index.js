loadPosts();

function loadPosts(searchKey) {
    axios.get('/api/PostsAPI', {
        params: {
            search: searchKey
        }
    })
        .then((res) => {        //se la richiesta va a buon fine
            console.log('risposta ok', res);
            if (res.data.length == 0) {     //non ci sono post da mostrare => nascondo la tabella
                document.getElementById('post-table').classList.add('d-none');
                document.getElementById('no-posts').classList.remove('d-none');
            } else {                        //ci sono post da mostrare => visualizzo la tabella
                document.getElementById('post-table').classList.remove('d-none');
                document.getElementById('no-posts').classList.add('d-none');

                //svuoto la tabella
                document.getElementById('posts').innerHTML = '';
                res.data.forEach(post => {
                    console.log('post', post);
                    document.getElementById('posts').innerHTML +=
                        `
                        <tr>
                            <td>
                                <a href="/Client/Detail?id=${post.id}">${post.id}</a>
                            </td>
                            <td class="image"><img src=${post.image}></td>
                            <td class="title">${post.title}</td>
                            <td class="description">${post.description}</td>
                            <td>
                                <a class="btn btn-primary" href="/Client/Edit?id=${post.id}">
                                <i class="fa-solid fa-pen-to-square"></i></a>
                            </td>
                            <td>
                                <a class="btn btn-danger" onclick="deletePost(${post.id})">
                                    <i class="fa-solid fa-trash"></i>
                                </a>                                
                            </td>
                        </tr>
                        `;
                })
            }
        })
        .catch((res) => {       //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });

}


function deletePost(postId) {
    const isDelete = confirm('Sei sicuro?');
    if (isDelete) {
        axios.delete(`/api/PostsAPI/${postId}`)
            .then((res) => {        //se la richiesta va a buon fine
                loadPosts();
            })
            .catch((res) => {
                console.error('errore', res);
                alert('errore nella richiesta');
            })
    }
}