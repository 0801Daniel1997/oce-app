import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";

const PlanProcedureItem = ({ procedure, users }) => {
   
    const savedUsers = JSON.parse(localStorage.getItem('selectedUsers')) || [];

    const [selectedUsers, setSelectedUsers] = useState(savedUsers);

    useEffect(() => {
      
        localStorage.setItem('selectedUsers', JSON.stringify(selectedUsers));
    }, [selectedUsers]);

    const handleAssignUser = (e) => {
        setSelectedUsers(e);
       
    };

    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={(e) => handleAssignUser(e)}
            />
        </div>
    );
};

export default PlanProcedureItem;