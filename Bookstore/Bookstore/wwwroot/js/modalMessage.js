function showModalWithButtons({ title = "", message = "", yesContent = "", noContent = "", onYes, onNo }) {
    const main = document.getElementById("modal");
    if (main) {
        const modal = document.createElement("div");
        modal.className = "custom-modal";

        const modalContent = document.createElement("div");
        modalContent.className = "custom-modal-content";

        const modalTitle = document.createElement("h3");
        modalTitle.textContent = title;

        const modalMessage = document.createElement("p");
        modalMessage.textContent = message;

        const btnYes = document.createElement("button");
        btnYes.className = "custom-btn custom-btn-yes";
        btnYes.textContent = yesContent;

        const btnNo = document.createElement("button");
        btnNo.className = "custom-btn custom-btn-no";
        btnNo.textContent = noContent;

        modalContent.appendChild(modalTitle);
        modalContent.appendChild(modalMessage);
        modalContent.appendChild(btnYes);
        modalContent.appendChild(btnNo);
        modal.appendChild(modalContent);
        main.appendChild(modal);

        modal.style.display = "block";

        btnYes.addEventListener("click", function () {
            if (onYes && typeof onYes === "function") {
                onYes();
            }
            closeModal();
        });

        btnNo.addEventListener("click", function () {
            if (onNo && typeof onNo === "function") {
                onNo();
            }
            closeModal();
        });

        function closeModal() {
            modal.style.display = "none";
            main.removeChild(modal);
        }
    }
}


