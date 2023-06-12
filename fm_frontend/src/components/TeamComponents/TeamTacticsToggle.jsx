import * as React from "react";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";

export default function VerticalToggleButtons({ teamTactic }) {
  const [view, setView] = React.useState(teamTactic);

  const handleChange = (event, nextView) => {
    setView(nextView);
  };

  return (
    <ToggleButtonGroup
      orientation="vertical"
      value={view}
      exclusive
      onChange={handleChange}
    >
      <ToggleButton value="attacking" aria-label="attacking">
        <p>Attacking</p>
      </ToggleButton>
      <ToggleButton value="balanced" aria-label="balanced">
        <p>Balanced</p>
      </ToggleButton>
      <ToggleButton value="defending" aria-label="defending">
        <p>Defending</p>
      </ToggleButton>
    </ToggleButtonGroup>
  );
}
