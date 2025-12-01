* i think is better to Create and Use Custom **List-View** or **Grid-View** for representing Each Entity as Graphical Card, that is, each item has its own default picture/sticker
	1. Student Picture
	2. Master Picture
	3. Admin/Manager Picture
* finally add "Sort By" option on Top-Right of the List-View or Grid-View and maybe Then-By option
* Use View's Represented in ViewRepo\<T> for presenting data to Card-View/Grid-View/List-View
* but that way i can't change them and for adding new item i should use Non-View Entity Repo's
* **Presentation** Says **What Lesson Presented by Master** in **Which Day of Week** and **When Presented (Time)**
* **Selections** Says **Which Students Selected What Presented Class** and What **Score** They Gain From that class and **Education Year**

* UI Include:
	1. **Manager-Window**
		1. *Masters-Page*
			* for CRUD Operations
		2. *Students-Page*
			* for CRUD Operations
		3. *Lessons-Page*
			* for CRUD Operations
		4. *Presentations-Page*
			* for CRUD Operations
		5. *Selections-Page*
			* for CRUD Operations
	2. **Student-Window**
		1. *Profile-Page*
			* Details like (Score, Name, Id, etc.)
		2. *Selections-Page*
			* Selected Lessons by this student
		3. *Presentations-Page*
			* Presented Lessons for this student
	3. **Master-Window**
		1. *Profile-Page*
			* Details like (Personal-Information, Picture, etc.)
		2. *Lessons-Page*
			* Lessons presented by this master
		3. *Presentations-Page*
			* Full Info About Presentations by this master

**Application Wide Properties:**
  -	user_mode
	1. admin
	1. guest
  - user_role
	1. student
	1. master
	1. manager
  - current_user
	- for Manager: Person() 
	- for Student: Student()
	- for Master: Master()