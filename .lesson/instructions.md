# COM741 Web Applications Development

## Practical 8

 
1. Carry out the following UI/UX improvements to the application
    * Replace/supplement text used in anchor tags on Index view with suitable bootstrap icons
    * Supplement button text in Details view with a suitable bootstrap icon
    * Add breadcrumbs to relevant views

2. The StudentController now inherits from BaseController rather than the default Controller class. The BaseController provides a new **Alert** method used to display visual alerts to the user. Make the following changes to the Student controller to use this new Alert functionality.
    * Remove all occurances of ```return NotFound()``` and replace with with an appropriate error/warning Alert, followed by a redirection to an appropriate action. For example in the Details action this might be
   
    ```
    Alert("Student Was Not Found", AlertType.info);
    return RedirectToAction(nameof(Index));
    ```

3. Modify the Details.cshtml view to use a Modal dialog that allows the user to confirm deletion of the student without having to navigate to a new view. Review the notes for an example showing how to create a delete modal dialog.

4. Complete the following methods in the TicketController.cs 

    ```
    IActionResult Close(int id) 
    IActionResult Create()
    IActionResult Create(Ticket tvm)
    ```

3. Complete the following views in the Views/Ticket folder
    * Index.cshtml – display list of tickets, each with close action & create ticket button
    * Create.cshtml – form to collect data to create a ticket that utilises a selectlist that displays a list of students. See notes for use of SelectList and TicketViewModel

4. Add a Navbar link to the Views/Shared/_Layout.cshtml view that provides a menu item to navigate to the Tickets controller Index action.
    * OPTIONAL - Combine the menu actions *About* and *Privacy* into a single dropdown titled *Company*. Review the notes and bootstrap documentation on how to add a drop down menu.


5. Use new custom validators
    * Replace the [Url] validator in Student.cs with our new [UrlResource] validator. Test the validator by trying to enter a valid url, but one that does not point to a resource.
    * Modify the StudentController Edit and Create Actions to check if the email address is a duplicate and add a validation error (see notes for example)
