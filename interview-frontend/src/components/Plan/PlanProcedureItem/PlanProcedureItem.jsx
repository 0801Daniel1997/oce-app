import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";

const PlanProcedureItem = ({ plan, procedure, users }) => {
    const [selectedUsers, setSelectedUsers] = useState([]);
    const api_url = "https://localhost:5001";
    const url1 = `${api_url}/ProcedureUser/AddUserToProcedure`;
    const url2 = `${api_url}/SelectedList?planId=${plan}`;
    const url3 = `${api_url}/SelectedList/ModifySelectedList`;

    useEffect(() => {
        getUserByPlan();
    }, [plan, procedure]);

    const handleSubmit = async () => {
        const planId = plan;
        const procedureId = procedure.procedureId;
        const userIds = selectedUsers.map(user => user.value);

        const command = {
            planId: planId,
            procedureId: procedureId,
            userIds: userIds
        };

        try {
            const response = await fetch(url1, {
                method: "POST",
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(command),
            });

            if (!response.ok) {
                const error = await response.text();

                throw new Error(`Failed to create plan: ${error}`);
            }

            alert("Saved Successfully!");
        } catch (error) {
            alert(error);
        }
    };

    const handleAssignUser = async (e) => {
        const addedUsers = e.filter(user => !selectedUsers.some(selected => selected.value === user.value));
        const removedUsers = selectedUsers.filter(user => !e.some(selected => selected.value === user.value));

        setSelectedUsers(e);

        for (const user of addedUsers) {
            await modifySelectedList(user.value, true);
        }

        for (const user of removedUsers) {
            await modifySelectedList(user.value, false);
        }
    };

    const modifySelectedList = async (userId, isChecked) => {
        const command = {
            planId: plan,
            procedureId: procedure.procedureId,
            userId: userId,
            isChecked: isChecked,
            status: 'Logged'
        };

        try {
            const response = await fetch(url3, {
                method: "POST",
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(command),
            });

            if (!response.ok) {
                const error = await response.text();
                throw new Error(`Failed to modify selected list: ${error}`);
            }


            getUserByPlan();
        } catch (error) {
            console.error("Error modifying selected list:", error);
        }
    };

    const getUserByPlan = async () => {
        try {
            const response = await fetch(url2, {
                method: "GET",
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                const error = await response.text();
                throw new Error(`Failed to fetch selected list: ${error}`);
            }

            const data = await response.json();



            const filteredData = data.filter(item =>
                item.procedureId === procedure.procedureId && item.isChecked
            );


            const mappedUsers = users.filter(user =>
                filteredData.some(selected => selected.userId === user.value)
            );

            setSelectedUsers(mappedUsers);
        } catch (error) {
            console.error("Error fetching selected list:", error);
        }
    };

    return (
        <div className="py-2">
            <div>{procedure.procedureTitle}</div>
            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={handleAssignUser}
            />
            <button onClick={handleSubmit}>
                Save and Submit
            </button>
        </div>
    );
};

export default PlanProcedureItem; 