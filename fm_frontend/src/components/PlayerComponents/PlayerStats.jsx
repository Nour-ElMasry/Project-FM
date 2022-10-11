import RatingProgressBar from "../RatingProgressBar";

const PlayerStats = (props) => {
    return <div className="playerStats container--pa container--styled">
        <h2>Player Stats</h2>
        <RatingProgressBar ratingType="Overall" value={props.player?.playerStats?.overallRating} />
        <RatingProgressBar ratingType="Attacking" value={props.player?.playerStats?.attacking} />
        <RatingProgressBar ratingType="PlayMaking" value={props.player?.playerStats?.playMaking} />
        <RatingProgressBar ratingType="Defending" value={props.player?.playerStats?.defending} />
        <RatingProgressBar ratingType="Goalkeeping" value={props.player?.playerStats?.goalkeeping} />
    </div>
}

export default PlayerStats;