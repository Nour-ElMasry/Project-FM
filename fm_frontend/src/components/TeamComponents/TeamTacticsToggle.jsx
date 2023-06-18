import * as React from "react";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";

export default function VerticalToggleButtons({
  teamTactic,
  handleTeamTacticChange,
}) {
  const [view, setView] = React.useState(teamTactic);

  const handleChange = (event, nextView) => {
    handleTeamTacticChange(nextView);
    setView(nextView);
  };

  return (
    <div style={{ textAlign: "center", marginLeft: "1.5rem" }}>
      <p className="ratingTitle">Team Tactics</p>
      <ToggleButtonGroup
        orientation="vertical"
        value={view}
        exclusive
        onChange={handleChange}
      >
        <ToggleButton
          value="attacking"
          aria-label="attacking"
          style={{ padding: "0.5rem" }}
        >
          <p>Att</p>
        </ToggleButton>
        <ToggleButton
          value="balanced"
          aria-label="balanced"
          style={{ padding: "0.5rem" }}
        >
          <p>Bal</p>
        </ToggleButton>
        <ToggleButton
          value="defending"
          aria-label="defending"
          style={{ padding: "0.5rem" }}
        >
          <p>Def</p>
        </ToggleButton>
      </ToggleButtonGroup>
    </div>
  );
}
