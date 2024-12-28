$('#btncancel').click(function () {
    clearform();
});
function clearform() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

$('#btnsave').click(function (e) {
    e.preventDefault();
    var l = Ladda.create(this);
        l.start();
    if (editblogid == null) {
        // Loading(true);
        setTimeout(() => {
            l.stop();
            // Loading(false);
            savedata();
        }, 3000);
        // savedata();
    }
    else {
        updatedata();
    }
    // l.stop();
});

function updatedata() {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    console.log({ title, author, content });
    lst = getdata();
    let index = lst.findIndex(x => x.id == editblogid);
    lst[index].title = title;
    lst[index].author = author;
    lst[index].content = content;
    const jsonstr = JSON.stringify(lst);
    localStorage.setItem('blogs', jsonstr);
    clearform();
    successMessage('Blog updating successfully');
    // alert('Blog updating successfully');
    loadData();
    editblogid = null;
}
function bindDeleteButtom() {
    $('.btn-delete').click(function () {
        // var result = confirm('Are you sure you want to delete this blog?');
        const id = this.dataset.blogId;
        confirmMessage('Are you sure you want to delete this blog?', function (result) {
            if (!result) return;
            lst = getdata();
            lst = lst.filter(x => x.id != id);
            const jsonstr = JSON.stringify(lst);
            localStorage.setItem('blogs', jsonstr);
            loadData();
            successMessage('Blog deleted successfully');
            // alert('Blog deleted successfully');
        });
    });
}
function savedata() {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    console.log({ title, author, content });
    lst = getdata();
    // let lst = localStorage.getItem('blogs');
    // if (lst == null) {
    //     lst = [];
    // }
    // else {
    //     lst = JSON.parse(lst);
    // }
    const blog = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };
    lst.push(blog);
    const jsonstr = JSON.stringify(lst);
    localStorage.setItem('blogs', jsonstr);
    clearform();
    successMessage('Blog saved successfully');
    // alert('Blog saved successfully');
    loadData();
}
let editblogid = null;
function bindEditButtom() {
    $('.btn-edit').click(function () {
        const id = this.dataset.blogId;
        //    console.log(id);
        lst = getdata();
        let filterlst = lst.filter(x => x.id == id);
        console.log(filterlst);
        if (filterlst.length == 0) {
            alert('No blog found');
            return;
        }
        let item = filterlst[0];
        $('#txtTitle').val(item.title);
        $('#txtAuthor').val(item.author);
        $('#txtContent').val(item.content);
        editblogid = item.id;
    });
}
function getdata() {
    let lst = localStorage.getItem('blogs');
    if (lst == null) {
        lst = [];
    }
    else {
        lst = JSON.parse(lst);
    }
    return lst;
}
function loadData() {
    lst = getdata();
    // let lst = localStorage.getItem('blogs');
    // if (lst == null) {
    //     lst = [];
    // }
    // else {
    //     lst = JSON.parse(lst);
    // }
    for (let i = 0; i < lst.length; i++) {
        const blog = lst[i];
        const html = `<tr>
            <td>
                <button type="button" class="btn btn-warning btn-edit" data-blog-id =${blog.id}>Edit</button>
                <button type="button" class="btn btn-danger btn-delete" data-blog-id =${blog.id}>Delete</button>
            </td>
            <td>${i + 1}</td>
            <td>${blog.title}</td>
            <td>${blog.author}</td>
            <td>${blog.content}</td>
        </tr>`;
        $('#tblBlogs').append(html);
    }
    bindEditButtom();
    bindDeleteButtom();


    new DataTable('#tbDatatable');
}

$(document).ready(function () {
    loadData();
    //test
    // let lst = localStorage.getItem('blogs');
    // if (lst == null) {
    //     lst = [];
    // }
    // else {
    //     lst = JSON.parse(lst);
    // }
    // const blog = {
    //     title: title,
    //     author: author,
    //     content: content
    // };
    // lst.push(blog);
    // const jsonstr = JSON.stringify(lst);
    // localStorage.setItem('blogs', jsonstr);
    // clearform();
    // alert('Blog saved successfully');
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        autoHide: true,

    });

});
function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}
// different between js and jquery
// document.getElementById('testing').value = 'Hello World 2';
// $('#testing').val('Hello World 3');

//local storage testing
// localStorage.setItem('testing', 'heee heeee');
// $('#testing').val(localStorage.getItem('testing'));

function successMessage(message) {
    // sweetalert test
    // Swal.fire({
    //     title: 'Success',
    //     text: message,
    //     icon: 'success',
    //     confirmButtonText: 'Cool'
    // })
    //Notiflix test
    // Notiflix.Notify.success(message);
    Notiflix.Report.success(
        'Success',
        message,
        'Okay',
    );
}
function confirmMessage(message, callback) {
    // Swal.fire({
    //     title: "Are you sure?",
    //     text: "You won't be able to revert this!",
    //     icon: "warning",
    //     showCancelButton: true,
    //     confirmButtonColor: "#3085d6",
    //     cancelButtonColor: "#d33",
    //     confirmButtonText: "Yes"
    //   }).then((result) => {
    //     callback(result.isConfirmed);
    //   });    

    Notiflix.Confirm.show(
        'Notiflix Confirm',
        'Do you agree with me?',
        'Yes',
        'No',
        function okCb() {
            callback(true);
        },
        function cancelCb() {
            callback(false);
        },
    );
}

function Loading(isloading) {
    if (isloading) {
        //Notiflix.Loading.circle();
        Notiflix.Block.standard('#loading', 'please wait...');
    } else {
        // Notiflix.Loading.remove();
        Notiflix.Block.remove('#loading', 2000);
    }
}