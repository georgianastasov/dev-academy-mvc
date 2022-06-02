// Login Register Change 
const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container1');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});


//Comments in About View
function goRight() {
	var box1 = document.getElementById('box1');
	var box2 = document.getElementById('box2');
	box1.style.display = "block";
	box2.style.display = "block";

	var block = document.getElementById('block');

	if (box1.style.display === 'block') {
		box1.style.display = 'none';
		box2.style.display = 'block';
		box3.style.display = 'none';
		block.style.backgroundColor = "#3A8EA2";
	}
	else if (box2.style.display === 'block') {
		box1.style.display = 'none';
		box2.style.display = 'none';
		box3.style.display = 'block';
		block.style.backgroundColor = "#71CFE2";
	}
}

function goLeft() {
	var box1 = document.getElementById('box1');
	var box2 = document.getElementById('box2');

	var block = document.getElementById('block');

	if (box2.style.display === 'block') {
		box1.style.display = 'block';
		box2.style.display = 'none';
		box3.style.display = 'none';
		block.style.backgroundColor = "#54C0A6";
	}
}


//Admin
function ShowUsers() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	users.style.display = "none";
	if (users.style.display === "none") {
		admins.style.display = "none";
		teachers.style.display = "none";
		students.style.display = "none";
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		users.style.display = "block";
	}
}

function ShowAdmins() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	admins.style.display = "none";
	if (admins.style.display === "none") {
		users.style.display = "none";
		teachers.style.display = "none";
		students.style.display = "none";
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		admins.style.display = "block";
	}
}

function ShowTeachers() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	teachers.style.display = "none";
	if (teachers.style.display === "none") {
		users.style.display = "none";
		admins.style.display = "none";
		students.style.display = "none";
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		teachers.style.display = "block";
	}
}

function ShowStudents() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	students.style.display = "none";
	if (students.style.display === "none") {
		users.style.display = "none";
		admins.style.display = "none";
		teachers.style.display = "none";
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		students.style.display = "block";
	}
}

function ShowCategories() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	categories.style.display = "none";
	if (categories.style.display === "none") {
		users.style.display = "none";
		admins.style.display = "none";
		teachers.style.display = "none";
		students.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		categories.style.display = "block";
	}
}

function ShowCourses() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	courses.style.display = "none";
	if (courses.style.display === "none") {
		users.style.display = "none";
		admins.style.display = "none";
		teachers.style.display = "none";
		students.style.display = "none";
		categories.style.display = "none";
		sections.style.display = "none";
		courses.style.display = "block";
	}
}

function ShowSections() {
	var users = document.getElementById("user-section");
	var admins = document.getElementById("admin-section");
	var teachers = document.getElementById("teacher-section");
	var students = document.getElementById("student-section");
	var categories = document.getElementById("category-section");
	var courses = document.getElementById("course-section");
	var sections = document.getElementById("section-section");
	sections.style.display = "none";
	if (sections.style.display === "none") {
		users.style.display = "none";
		admins.style.display = "none";
		teachers.style.display = "none";
		students.style.display = "none";
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "block";
	}
}

//Teacher
function ShowCategoriess() {
	var categories = document.getElementById("teacher-categories");
	var courses = document.getElementById("teacher-courses");
	var sections = document.getElementById("teacher-sections");
	var students = document.getElementById("teacher-students");
	var profile = document.getElementById("teacher-profile");
	categories.style.display = "none";
	if (categories.style.display === "none") {
		categories.style.display = "block";
		courses.style.display = "none";
		sections.style.display = "none";
		students.style.display = "none";
		profile.style.display = "none";
	}
}

function ShowCoursess() {
	var categories = document.getElementById("teacher-categories");
	var courses = document.getElementById("teacher-courses");
	var sections = document.getElementById("teacher-sections");
	var students = document.getElementById("teacher-students");
	var profile = document.getElementById("teacher-profile");
	courses.style.display = "none";
	if (courses.style.display === "none") {
		categories.style.display = "none";
		courses.style.display = "block";
		sections.style.display = "none";
		students.style.display = "none";
		profile.style.display = "none";
	}
}

function ShowSectionss() {
	var categories = document.getElementById("teacher-categories");
	var courses = document.getElementById("teacher-courses");
	var sections = document.getElementById("teacher-sections");
	var students = document.getElementById("teacher-students");
	var profile = document.getElementById("teacher-profile");
	sections.style.display = "none";
	if (sections.style.display === "none") {
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "block";
		students.style.display = "none";
		profile.style.display = "none";
	}
}

function ShowStudentss() {
	var categories = document.getElementById("teacher-categories");
	var courses = document.getElementById("teacher-courses");
	var sections = document.getElementById("teacher-sections");
	var students = document.getElementById("teacher-students");
	var profile = document.getElementById("teacher-profile");
	students.style.display = "none";
	if (students.style.display === "none") {
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		students.style.display = "flex";
		profile.style.display = "none";
	}
}

function ShowProfilee() {
	var categories = document.getElementById("teacher-categories");
	var courses = document.getElementById("teacher-courses");
	var sections = document.getElementById("teacher-sections");
	var students = document.getElementById("teacher-students");
	var profile = document.getElementById("teacher-profile");
	profile.style.display = "none";
	if (profile.style.display === "none") {
		categories.style.display = "none";
		courses.style.display = "none";
		sections.style.display = "none";
		students.style.display = "none";
		profile.style.display = "block";
	}
}


//Student
function ShowCourse() {
	var course = document.getElementById("student-course");
	var progress = document.getElementById("student-progress");
	var profile = document.getElementById("student-profile");
	course.style.display = "none";
	if (course.style.display === "none") {
		profile.style.display = "none";
		progress.style.display = "none";
		course.style.display = "flex";
	}
}

function ShowProgress() {
	var course = document.getElementById("student-course");
	var progress = document.getElementById("student-progress");
	var profile = document.getElementById("student-profile");
	progress.style.display = "none";
	if (progress.style.display === "none") {
		course.style.display = "none";
		profile.style.display = "none";
		progress.style.display = "flex";
	}
}

function ShowProfile() {
	var course = document.getElementById("student-course");
	var progress = document.getElementById("student-progress");
	var profile = document.getElementById("student-profile");
	profile.style.display = "none";
	if (profile.style.display === "none") {
		course.style.display = "none";
		progress.style.display = "none";
		profile.style.display = "block";
	}
}

//Bio
function ShowBio() {
	var bio = document.getElementById("box-form-box");
	bio.style.display = "none";
	if (bio.style.display === "none") {
		bio.style.display = "block";
	}
}

function HideBio() {
	var bio = document.getElementById("box-form-box");
	bio.style.display = "block";
	if (bio.style.display === "block") {
		bio.style.display = "none";
	}
}

//Profile Edit Student
function ShowEdit() {
	var profile = document.getElementById("box-profile-box");
	profile.style.display = "none";
	if (profile.style.display === "none") {
		profile.style.display = "block";
	}
}

function HideEdit() {
	var profile = document.getElementById("box-profile-box");
	profile.style.display = "block";
	if (profile.style.display === "block") {
		profile.style.display = "none";
	}
}

//Profile Edit Teacher
function ShowEditt() {
	var profile = document.getElementById("box-profile-boxx");
	profile.style.display = "none";
	if (profile.style.display === "none") {
		profile.style.display = "block";
	}
}

function HideEditt() {
	var profile = document.getElementById("box-profile-boxx");
	profile.style.display = "block";
	if (profile.style.display === "block") {
		profile.style.display = "none";
	}
}