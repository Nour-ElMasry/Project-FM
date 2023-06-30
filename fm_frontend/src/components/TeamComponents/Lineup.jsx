import React from "react";
import "./Lineup.css";
import RatingSpan from "../RatingSpan";
import { Link } from "react-router-dom";

const Lineup = ({ startingEleven, bench }) => {
  const comparePositions = (a, b) => {
    const positionsOrder = {
      Goalkeeper: 1,
      Defender: 2,
      Midfielder: 3,
      Attacker: 4,
    };

    const positionA = positionsOrder[a.position];
    const positionB = positionsOrder[b.position];

    if (positionA < positionB) {
      return -1;
    } else if (positionA > positionB) {
      return 1;
    } else {
      return 0;
    }
  };

  const renderPlayerCard = (player) => {
    let playerNameSplit = player.playerPerson.name.split(" ");
    let playerName = playerNameSplit[0];
    if (playerNameSplit.length > 1) {
      playerName = playerNameSplit[1];
    }
    return (
      <Link
        to={`/Players/${player.id}`}
        key={player.id}
        className="player-card"
      >
        <div className="pic-rating">
          <img src={player.playerPerson.image} alt={player.playerPerson.name} />
          <RatingSpan rating={player.playerStats.overallRating} overall />
        </div>
        <div className="player-info">
          <p style={{ fontWeight: "bold", fontSize: "16px" }}>{playerName}</p>
          <p>{player.playerPerson.country}</p>
        </div>
      </Link>
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
          <div className="pitch-row goalkeepers">{goalkeepersRow}</div>
          <div className="pitch-row defenders">{defendersRow}</div>
          <div className="pitch-row midfielders">{midfieldersRow}</div>
          <div className="pitch-row attackers">{attackersRow}</div>
        </div>
      </div>
    );
  };

  const renderBench = () => {
    return (
      <div className="bench">
        <h2>Bench</h2>
        <ul className="player-list">
          {bench.sort(comparePositions).map((player) => {
            let playerNameSplit = player.playerPerson.name.split(" ");
            let playerName = playerNameSplit[0];
            if (playerNameSplit.length > 1) {
              playerName = playerNameSplit[1];
            }
            return (
              <li key={player.id} className="bench-player">
                <Link to={`/Players/${player.id}`} className="player-link">
                  <RatingSpan
                    rating={player.playerStats.overallRating}
                    overall
                  />
                  <img
                    src={player.playerPerson.image}
                    alt={playerName}
                    className="player-image"
                  />
                  <div className="player-details">
                    <p className="player-name">{playerName}</p>
                    <p className="player-name">{player.position}</p>
                  </div>
                </Link>
              </li>
            );
          })}
        </ul>
      </div>
    );
  };

  const renderStartingList = () => {
    return (
      <div className="starting bench">
        <h2>Starting Eleven</h2>
        <ul className="player-list">
          {startingEleven.sort(comparePositions).map((player) => {
            let playerNameSplit = player.playerPerson.name.split(" ");
            let playerName = playerNameSplit[0];
            if (playerNameSplit.length > 1) {
              playerName = playerNameSplit[1];
            }
            return (
              <li key={player.id} className="bench-player">
                <Link to={`/Players/${player.id}`} className="player-link">
                  <RatingSpan
                    rating={player.playerStats.overallRating}
                    overall
                  />
                  <img
                    src={player.playerPerson.image}
                    alt={playerName}
                    className="player-image"
                  />
                  <div className="player-details">
                    <p className="player-name">{playerName}</p>
                    <p className="player-name">{player.position}</p>
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
      {renderStartingList()}
      {renderBench()}
    </div>
  );
};

export default Lineup;
