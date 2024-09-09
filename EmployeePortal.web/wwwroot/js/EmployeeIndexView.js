function ToggleDarkMode() {
    // Get references to the body, table container, and toggle button elements
    const body = document.body;
    const tableContainer = document.getElementById("tableContainer");
    const toggleButton = document.getElementById("toggleMode");

    // Check if the body already has the "dark-mode" class
    if (body.classList.contains("dark-mode")) {
        // If in dark mode, switch to light mode
        body.classList.remove("dark-mode");
        body.classList.add("light-mode");
        tableContainer.classList.remove("dark-mode");
        tableContainer.classList.add("light-mode");
        toggleButton.textContent = "Dark Mode";
    } else {
        // If in light mode, switch to dark mode
        body.classList.remove("light-mode");
        body.classList.add("dark-mode");
        tableContainer.classList.remove("light-mode");
        tableContainer.classList.add("dark-mode");
        toggleButton.textContent = "Light Mode";
    }
}


// **Handle "Clear Search" button click**

const searchInput = document.getElementById("searchInput");
const cancelButton = document.getElementById("cancelButton");
const searchButton = document.querySelector('.btn-dark[type="submit"]');

// Initial state: Show cancel button if search input has value
if (searchInput.value.trim() !== "") {
    cancelButton.style.display = "block";
}

// Attach event listeners to search input and search button
searchInput.addEventListener("input", () => {
    if (searchInput.value.trim() !== "") {
        cancelButton.style.display = "block";
    } else {
        cancelButton.style.display = "none";
    }
});

searchButton.addEventListener("click", () => {
    // Toggle cancel button visibility on search button click
    if (cancelButton.style.display === "none") {
        cancelButton.style.display = "block";
    } else {
        cancelButton.style.display = "none";
    }
});

