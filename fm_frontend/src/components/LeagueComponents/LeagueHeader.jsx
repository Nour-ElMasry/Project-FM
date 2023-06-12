const LeagueHeader = (props) => {
  return (
    <div className="globalHeaderInfo">
      <img
        className="globalImg"
        src={props.league?.leagueLogo}
        alt={props.league?.name}
      />
      <div className="globalName">
        <h2>{props.league?.name}</h2>
        <p style={{ fontWeight: "bold" }}>
          {props.league?.currentSeason?.year}
        </p>
      </div>
    </div>
  );
};

export default LeagueHeader;
