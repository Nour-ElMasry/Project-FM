import RatingProgress from "../RatingProgress";
import { useState } from "react";
import VerticalToggleButtons from "./TeamTacticsToggle";

const TeamHeader = (props) => {
  const [userTeamId] = useState(JSON.parse(localStorage.getItem("UserTeamId")));

  if (userTeamId == props.teamId) {
    console.log("same");
  }

  return (
    <div>
      <div className="teamHeaderTeamInfo">
        <div className="info">
          <img src={props.teamLogo} alt="Team logo" />
          <div className="teamHeaderTeamName">
            <p>Team</p>
            <h2>{props.teamName}</h2>
          </div>
        </div>
        <div className="teamHeaderRatings">
          <p className="ratingTitle">Rating</p>
          <RatingProgress
            className="teamHeaderRatingsRate"
            title="Attacking"
            value={props.ratings.attackingRating}
          />
          <RatingProgress
            className="teamHeaderRatingsRate"
            title="Defending"
            value={props.ratings.defendingRating}
          />
        </div>
        <VerticalToggleButtons teamTactic={props.teamTactic} />
      </div>
      <div></div>
    </div>
  );
};

export default TeamHeader;
