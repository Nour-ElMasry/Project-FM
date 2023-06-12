import { useState } from "react";
import "./Carousel.css";

const Carousel = ({ teams, setSelectedTeam }) => {
  const [currentTeam, setCurrentTeam] = useState(0);

  const nextTeam = (e) => {
    e.preventDefault();
    let newIndex = (currentTeam + 1) % teams.length;
    setCurrentTeam(newIndex);
    setSelectedTeam({
      teamId: teams[newIndex].teamId,
      teamName: teams[newIndex].teamName,
    });
  };

  const prevTeam = (e) => {
    e.preventDefault();
    let newIndex = (currentTeam - 1 + teams.length) % teams.length;
    setCurrentTeam(newIndex);
    setSelectedTeam({
      teamId: teams[newIndex].teamId,
      teamName: teams[newIndex].teamName,
    });
  };

  return (
    <div className="carousel">
      <button className="carousel-button" onClick={prevTeam}>
        &lt;
      </button>
      <div className="team-container">
        <img
          className="team-badge"
          src={teams[currentTeam].teamLogo}
          alt={teams[currentTeam].name}
        />
        <p className="team-name">{teams[currentTeam].teamName}</p>
      </div>
      <button className="carousel-button" onClick={nextTeam}>
        &gt;
      </button>
    </div>
  );
};

export default Carousel;
