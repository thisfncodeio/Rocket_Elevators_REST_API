# Rocket_Elevators_REST_API

<p>
In order to connect our information system to the equipment in operation throughout the territory served, we first need to develop a REST API using C # and .NET Core.
It will allow us to know and to manipulate the status of all the relevant entities of the operational database.
</p>

<ol>
   <li>Retrieving the current status of a specific Battery</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/batteries/1/status</p>
   
   <li>Changing the status of a specific Batter</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/batteries/1</p>
   <p>Edit in postman: { "status": "Inactive" }</p>

   <li>Retrieving the current status of a specific Column</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/columns/1/status</p>
   
   <li>Changing the status of a specific Column</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/columns/1</p>
   <p>Edit in postman: { "status": "Inactive" }</p>

   <li>Retrieving the current status of a specific Elevator</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/elevators/1/status</p>

   <li>Changing the status of a specific Elevator</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/elevators/1</p>
   <p>Edit in postman:{ "status": "Inactive" }</p>

   <li>Retrieving a list of Elevators that are not in operation at the time of the request</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/elevators/inactive</p>
   
   <li>Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/buildings</p>
   
   <li>Retrieving a list of Leads created in the last 30 days who have not yet become customers.</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/leads/recent</p>
   
   <li>Returns all fields of all intervention Request records that do not have a start date and are in "Pending" status.</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/pending</p>
   
   <li>Changes the status of the intervention request to "InProgress" and adds a start date and time.</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/start/1</p>
   
   <li>Changes the status of the request for action to "Completed" and adds an end date and time.</li>
   <p>https://rocket-elevators-rest-apii.herokuapp.com/complete/1</p>
</ol>
