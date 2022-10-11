const PlayerRecord = (props) => {
    return <div className="playerRecord container--pa container--styled">
        <h2>Player Record</h2>
        {console.log(props.player)}
        <div className="recordContainer">
            <div className="recordContainerTop flex flex-ai-center">
                <div className="recordContainerTopLeft">
                    <img className="teamLogo" src={props.player?.currentTeam?.teamLogo} alt="Team"/>
                    <h4>{props.player?.currentTeam?.teamName}</h4>
                </div>
                <div className="recordContainerTopRight">

                </div>
            </div>
            <div className="recordContainerBottom">

            </div>
        </div>
    </div>
}

export default PlayerRecord;