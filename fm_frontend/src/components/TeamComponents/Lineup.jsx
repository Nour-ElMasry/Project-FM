import React from "react";
import "./Lineup.css";
import RatingSpan from "../RatingSpan";
import { Link } from "react-router-dom";

const Lineup = ({ startingEleven, bench }) => {
  const renderPlayerCard = (player) => {
    let playerNameSplit = player.playerPerson.name.split(" ");
    let playerName = playerNameSplit[0];
    if (playerNameSplit.length > 1) {
      playerName = playerNameSplit[1];
    }
    return (
      <div key={player.id} className="player-card">
        <div className="pic-rating">
          <img src={player.playerPerson.image} alt={player.playerPerson.name} />
          <RatingSpan rating={player.playerStats.overallRating} overall />
        </div>
        <div className="player-info">
          <Link to={`/Players/${player.id}`} style={{ color: "black" }}>
            <p style={{ fontWeight: "bold", fontSize: "16px" }}>{playerName}</p>
          </Link>
          <p>{player.playerPerson.country}</p>
        </div>
      </div>
    );
  };

  const renderLineup = () => {
    const defendersRow = startingEleven
      .filter((player) => player.position === "Defender")
      .map((player) => renderPlayerCard(player));

    const midfieldersRow = startingEleven
      .filter((player) => player.position === "Midfielder")
      .map((player) => renderPlayerCard(player));

    const attackersRow = startingEleven
      .filter((player) => player.position === "Attacker")
      .map((player) => renderPlayerCard(player));

    const goalkeepersRow = startingEleven
      .filter((player) => player.position === "Goalkeeper")
      .map((player) => renderPlayerCard(player));

    return (
      <div className="lineup">
        <div className="pitch">
          <div className="pitch-row attackers">{attackersRow}</div>
          <div className="pitch-row midfielders">{midfieldersRow}</div>
          <div className="pitch-row defenders">{defendersRow}</div>
          <div className="pitch-row goalkeepers">{goalkeepersRow}</div>
        </div>
      </div>
    );
  };

  const renderBench = () => {
    return (
      <div className="bench">
        <h2>Bench</h2>
        <ul className="bench-list">
          {bench.map((player) => {
            let playerNameSplit = player.playerPerson.name.split(" ");
            let playerName = playerNameSplit[0];
            if (playerNameSplit.length > 1) {
              playerName = playerNameSplit[1];
            }
            return (
              <li key={player.id} className="bench-player">
                <Link to={`/Players/${player.id}`} className="player-link">
                  <img
                    src={player.playerPerson.image}
                    alt={playerName}
                    className="player-image"
                  />
                  <div className="player-details">
                    <p className="player-name">{playerName}</p>
                  </div>
                </Link>
              </li>
            );
          })}
        </ul>
      </div>
    );
  };

  return (
    <div className="lineup-container">
      {renderLineup()}
      {renderBench()}
    </div>
  );
};

export default Lineup;
