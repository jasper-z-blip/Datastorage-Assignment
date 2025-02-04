import React, { useState } from 'react';

const CreateProject = ({ refreshProjects }) => {
    const [projectTitle, setProjectTitle] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [status, setStatus] = useState("Ej påbörjat");

    // Den här funktionen hanterar skapandet av ett projekt
    const handleCreateProject = async () => {
        console.log("Button PUSHED");
        const newProject = {
            title: projectTitle,    // Projektnamn
            startDate: startDate,   // Startdatum
            endDate: endDate,       // Slutdatum
            statusId: 1,            // StatusId
            customerId: 1,          // Kunde ID
            productId: 1,           // Produkt ID
            userId: 1               // Användare ID
        };

        console.log("Sending data to backend:", newProject);

        try {
            const response = await fetch("http://localhost:5197/api/projects", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newProject),
            });

            console.log(response.status);

            if (response.ok) {
                const createdProject = await response.json();  // Hämta det skapade projektet från servern
                alert("Projektet skapades!");
                setProjectTitle(""); // Rensa projektnamnet
                setStartDate("");    // Rensa startdatumet
                setEndDate("");      // Rensa slutdatumet
                setStatus("Ej påbörjat"); // Återställ status till "Ej påbörjat"

                refreshProjects(createdProject);
            } else {
                alert("Misslyckades med att skapa projekt");
                console.error("API-fel, svar:", response.status);
            }
        } catch (error) {
            console.error("Fel vid API-anrop:", error);
        }
    };

    return (
        <div>
            <h2>Skapa nytt projekt</h2>
            <input
                type="text"
                placeholder="Projektnamn"
                value={projectTitle}
                onChange={(e) => setProjectTitle(e.target.value)}
            />
            <input
                type="date"
                value={startDate}
                onChange={(e) => setStartDate(e.target.value)}
            />
            <input
                type="date"
                value={endDate}
                onChange={(e) => setEndDate(e.target.value)}
            />
            <select value={status} onChange={(e) => setStatus(e.target.value)}>
                <option value="Ej påbörjat">Ej påbörjat</option>
                <option value="Pågående">Pågående</option>
                <option value="Avslutat">Avslutat</option>
            </select>
            <button onClick={handleCreateProject}>Skapa projekt</button>
        </div>
    );
};

export default CreateProject;


// Delvis AI gererad kod