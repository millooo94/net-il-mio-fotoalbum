const loadPhotos = filter => getPhotos(filter).then(renderPhotos);

const getPhotos = title => axios
    .get('/api/photos', title ? { params: { title } } : {})
    .then(res => res.data);

const renderPhotos = photos => {
    const noPhotos = document.querySelector("#no-photos");
    const loader = document.querySelector("#photos-loader");
    const photosBody = document.querySelector("#photos");
    const photoFilter = document.querySelector("#photos-filter");

    if (photos && photos.length > 0) {
        photoFilter.classList.add("show");
        noPhotos.classList.remove("show");
    }
    else noPhotos.classList.add("show");

    loader.classList.add("hide");

    photosBody.innerHTML = photos.map(photoComponent).join('');
};

const photoComponent = photo => `
        <div class="col">
            <div class="card shadow-sm">
                <img class="bd-placeholder-img card-img-top" width="100%" height="225" src="${photo.imageUrl}"></img>

                <div class="card-body">
                    <h1>${photo.title}</h1>
                    <p class="card-text">
                        ${photo.description}
                    </p>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="btn-group">
                            <button type="button" class="btn btn-sm btn-outline-secondary">Acquista</button>
                        </div>
                        <small class="text-muted">9 minuti fa</small>
                    </div>
                </div>
            </div>
        </div>`;

const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};



const postContact = contact => axios
    .post("/api/contacts", contact)
    .then(() => location.href = "/home")
    .catch(err => renderErrors(err.response.data.errors));

const initCreateForm = () => {
    const form = document.querySelector("#contact-create-form");

    form.addEventListener("submit", e => {
        e.preventDefault();

        const contact = getContactFromForm(form);
        postContact(contact);
    });
};

const getContactFromForm = form => {
    const email = form.querySelector("#email").value;
    const message = form.querySelector("#message").value;

    return {
        id: 0,
        email,
        message,
    };
};

const renderErrors = errors => {
    const emailErrors = document.querySelector("#email-errors");
    const messageErrors = document.querySelector("#message-errors");

    emailErrors.innerText = errors.email?.join("\n") ?? "";
    messageErrors.innerText = errors.message?.join("\n") ?? "";
};