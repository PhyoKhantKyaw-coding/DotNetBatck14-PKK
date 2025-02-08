const API_ENDPOINTS = {
    LOGIN: "https://localhost:7297/api/Home/sign-in",
    REGISTER_USER: "https://localhost:7297/api/Home/register-user",
    REGISTER_ADMIN: "https://localhost:7297/api/Home/register-admin",
    USER_PROFILE: "https://localhost:7297/api/Home/User-id", // Endpoint to fetch user profile
    TRAVEL_PACKAGES: "https://localhost:7297/api/home/travel-packages",
    TRAVEL_PACKAGE_DETAILS: (id) => `https://localhost:7297/api/home/travel-package/${id}`,
    BOOK_TRAVEL_PACKAGE: "https://localhost:7297/api/User/bookings",
    MAKE_PAYMENT: "https://localhost:7297/api/User/payments"
};

let isLoggedIn = false; // Track login status
let currentUser = null; // Store the logged-in user's data
document.addEventListener('DOMContentLoaded', function () {
    checkLoginStatus();
});
// Function to check if the user is logged in
async function checkLoginStatus() {
    const userData = JSON.parse(localStorage.getItem('user')); // Retrieve stored user data
    if (userData) {
        isLoggedIn = true;
        currentUser = userData;
        updateProfileView(userData); // Update the profile view with the stored user data
    } else {
        isLoggedIn = false;
        currentUser = null;
        updateProfileView(null);
    }
}
// Function to update the profile view based on login status
function updateProfileView(userData) {
    const profileMessage = document.getElementById("profile-message");
    const profileData = document.getElementById("profile-data");
    const profileName = document.getElementById("profile-name");
    const profileEmail = document.getElementById("profile-email");
    const profilePhone = document.getElementById("profile-phone");

    // Check if elements are found
    if (!profileMessage || !profileData || !profileName || !profileEmail || !profilePhone) {
        console.error("One or more required elements are missing in the DOM.");
        return; // Exit if the elements are not found
    }
    if (userData) {
        profileMessage.style.display = "none";
        profileData.style.display = "block";
        profileName.textContent = userData.name;
        profileEmail.textContent = userData.email;
        profilePhone.textContent = userData.phone || "No phone number available";
    } else {
        profileMessage.style.display = "block";
        profileData.style.display = "none";
    }
}
// Show login/register modal when profile icon is clicked
document.getElementById("profile-icon-btn").addEventListener("click", () => {
    if (!isLoggedIn) {
        authModal.style.display = 'block';
    } else {
        const profileView = document.getElementById("profile-view");
        profileView.style.display = profileView.style.display === 'block' ? 'none' : 'block';
    }
});
// Close auth modal when clicking outside
window.addEventListener('click', (event) => {
    if (event.target === authModal) {
        authModal.style.display = 'none';
    }
});
// Login/Register Form Toggle
const toggleForm = document.getElementById('toggle-form');
const formTitle = document.getElementById('form-title');
const nameField = document.getElementById('name-field');
const phoneField = document.getElementById('phone-field');
const submitBtn = document.getElementById('submit-btn');
const authForm = document.getElementById('auth-form');
let isRegistering = false;
toggleForm.addEventListener('click', () => {
    isRegistering = !isRegistering;
    if (isRegistering) {
        formTitle.textContent = "Register";
        nameField.style.display = "block";
        phoneField.style.display = "block";
        submitBtn.textContent = "Register";
        toggleForm.textContent = "Already have an account? Login here";
    } else {
        formTitle.textContent = "Login";
        nameField.style.display = "none";
        phoneField.style.display = "none";
        submitBtn.textContent = "Sign In";
        toggleForm.textContent = "Don't have an account? Register here";
    }
});
// Handle form submission
authForm.addEventListener('submit', async (event) => {
    event.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const name = document.getElementById('name').value;
    const phone = document.getElementById('phone').value;
    const requestData = isRegistering
        ? { Name: name, Email: email, PasswordHash: password, Phone: phone }
        : { Email: email, Password: password };
    const endpoint = isRegistering ? API_ENDPOINTS.REGISTER_USER : API_ENDPOINTS.LOGIN;
    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(requestData)
        });
        if (!response.ok) {
            alert("Error: Unable to complete the request.");
        } else {
            const data = await response.json();
            if (data.success) { // Check for success
                localStorage.setItem('user', JSON.stringify(data.data)); // Save user data to localStorage
                await checkLoginStatus(); // Update login status and profile view
                authModal.style.display = 'none';
            } else {
                alert(data.message || "Authentication failed.");
            }
        }
    } catch (error) {
        console.error("Error occurred:", error);
    }
});
// Logout functionality
document.getElementById("logout-btn").addEventListener("click", () => {
    localStorage.removeItem('user'); // Remove user data
    isLoggedIn = false;
    currentUser = null;
    updateProfileView(null); // Update profile view to show logged-out state
    document.getElementById("profile-view").style.display = "none"; // Hide profile view
});
// Check login status on page load
document.addEventListener("DOMContentLoaded", async () => {
    await checkLoginStatus();
    fetch(API_ENDPOINTS.TRAVEL_PACKAGES)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => displayPackages(data))
        .catch(error => console.error("Error fetching travel packages:", error));
});
// Function to display travel packages
function displayPackages(packages) {
    const packageContainer = document.getElementById("travel-packages").querySelector(".row");
    packageContainer.innerHTML = "";
    packages.forEach(pkg => {
        const imageUrl = pkg.photoPath ? `https://localhost:7297${pkg.photoPath}` : '/images/default.jpg';
        let card = document.createElement("div");
        card.classList.add("col-md-4", "mb-3");
        card.innerHTML = `
    <div class="card" data-id="${pkg.id}">
        <img src="${imageUrl}" class="img-fluid" alt="Package Image">
        <div class="card-body">
            <h5 class="card-title">${pkg.title}</h5>
            <p class="card-text">${pkg.destination}</p>
            <button class="btn btn-primary booking-btn" onclick="viewDetails('${pkg.id}')">View Details</button>
        </div>
    </div>
`;
        packageContainer.appendChild(card);
    });
}
// Function to view package details
async function viewDetails(packageId) {
    if (!isLoggedIn) {
        authModal.style.display = 'block';
        return;
    }
    try {
        const response = await fetch(API_ENDPOINTS.TRAVEL_PACKAGE_DETAILS(packageId));
        if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);
        const data = await response.json();
        document.getElementById("modalImage").src = data.photoPath ? `https://localhost:7297${data.photoPath}` : '/images/default.jpg';
        document.getElementById("modalTitle").textContent = data.title;
        document.getElementById("modalDescription").textContent = data.description;
        document.getElementById("modalStartDate").textContent = data.startDate;
        document.getElementById("modalEndDate").textContent = data.endDate;
        document.getElementById("modalInclusions").textContent = data.inclusions;
        document.getElementById("modalStatus").textContent = data.status;
        document.getElementById("modalCancel").textContent = data.cancellationPolicy;
        document.getElementById("bookNowButton").setAttribute("data-package-id", packageId);
        new bootstrap.Modal(document.getElementById("packageModal")).show();
    } catch (error) {
        console.error("Error fetching package details:", error);
    }
}
// Close modal when clicking the close button
document.getElementById("close-modal").addEventListener("click", () => {
    authModal.style.display = "none";
});


// Function to open the Booking modal from View Details modal
document.getElementById('packageModal').addEventListener('shown.bs.modal', function () {
    const bookingButton = document.querySelector('#packageModal .btn-primary');
    bookingButton.addEventListener('click', function () {
        const packageModal = bootstrap.Modal.getInstance(document.getElementById('packageModal'));
        packageModal.hide(); // Close View Details modal first

        packageModal._element.addEventListener('hidden.bs.modal', function () {
            const bookingModal = new bootstrap.Modal(document.getElementById('bookingModal'));
            bookingModal.show();
        }, { once: true }); // Ensure event runs only once
    });
});

// Function to open the Payment modal from Booking modal
document.getElementById('bookingModal').addEventListener('shown.bs.modal', function () {
    const paymentButton = document.getElementById('proceedToPayment');
    paymentButton.addEventListener('click', function () {
        const bookingModal = bootstrap.Modal.getInstance(document.getElementById('bookingModal'));
        bookingModal.hide(); // Close Booking modal first

        bookingModal._element.addEventListener('hidden.bs.modal', function () {
            const paymentModal = new bootstrap.Modal(document.getElementById('paymentModal'));
            paymentModal.show();
        }, { once: true });
    });
});

let travelers = [];  
document.getElementById('addTravelerBtn').addEventListener('click', function() {
    const travelerName = document.getElementById('travelerName').value;
    const travelerAge = document.getElementById('travelerAge').value;
    const travelerGender = document.getElementById('travelerGender').value;

    if (!travelerName || !travelerAge || !travelerGender) {
        alert("Please fill out all traveler fields");
        return;
    }
    const traveler = {
        name: travelerName,
        age: parseInt(travelerAge),
        gender: travelerGender
    };
    travelers.push(traveler);
    document.getElementById('travelerName').value = '';
    document.getElementById('travelerAge').value = '';
    document.getElementById('travelerGender').value = 'Male';
    displayTravelerList();
});

// function displayTravelerList() {
//     const travelerListDiv = document.getElementById('travelerList');
//     travelerListDiv.innerHTML = '';  
//     travelers.forEach((traveler, index) => {
//         const travelerDiv = document.createElement('div');
//         travelerDiv.classList.add('traveler-item');
//         travelerDiv.innerHTML = `
//             <p>${traveler.name}, ${traveler.age} years old, ${traveler.gender}</p>
//             <button class="btn btn-danger btn-sm" onclick="removeTraveler(${index})">Remove</button>
//         `;
//         travelerListDiv.appendChild(travelerDiv);
//     });
// }
function displayTravelerList() {
    const travelerListDiv = document.getElementById('travelerList');
    travelerListDiv.innerHTML = '';  

    if (travelers.length === 0) {
        travelerListDiv.innerHTML = '<p>No travelers added yet.</p>';
        return;
    }

    // Create table element
    const table = document.createElement('table');
    table.classList.add('table', 'table-bordered', 'mt-2');

    // Table header
    table.innerHTML = `
        <thead class="thead-light">
            <tr>
                <th>#</th>
                <th>Name</th>
                <th>Age</th>
                <th>Gender</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody id="travelerTableBody"></tbody>
    `;

    const tbody = table.querySelector('#travelerTableBody');

    // Populate table rows
    travelers.forEach((traveler, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${index + 1}</td>
            <td>${traveler.name}</td>
            <td>${traveler.age}</td>
            <td>${traveler.gender}</td>
            <td>
                <button class="btn btn-danger btn-sm" onclick="removeTraveler(${index})">Remove</button>
            </td>
        `;
        tbody.appendChild(row);
    });

    travelerListDiv.appendChild(table);
}

document.getElementById('confirmPayment').addEventListener('click', async function() {
    const userId = currentUser.id;  
    const travelPackageId = document.getElementById('bookNowButton').getAttribute('data-package-id');
    const bookingData = {
        userId: userId,
        travelPackageId: travelPackageId,
        travelers: travelers
    };
    try {
        const response = await fetch(API_ENDPOINTS.BOOK_TRAVEL_PACKAGE, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(bookingData)
        });

        const data = await response.json();
        if (data.success === "true") {
            const bookingId = data.data.id;
            const totalPrice = data.data.totalPrice;
            await makePayment(userId, bookingId, totalPrice);
        } else {
            alert(data.message || "Failed to create booking");
        }
    } catch (error) {
        console.error('Error:', error);
    }
});

function removeTraveler(index) {
    travelers.splice(index, 1);  // Remove traveler from list
    displayTravelerList();  // Re-render traveler list
}
async function makePayment(userId, bookingId, amount) {
    const paymentData = {
        userId: userId,
        bookingId: bookingId,
        amount: amount
    };
    try {
        const response = await fetch(API_ENDPOINTS.MAKE_PAYMENT, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(paymentData)
        });
        const data = await response.json();
        if (data.success === "true") {
            alert("Payment added successfully!");
        } else {
            alert(data.message || "Failed to process payment");
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

