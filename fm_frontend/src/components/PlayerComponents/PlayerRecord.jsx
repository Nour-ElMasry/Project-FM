const PlayerRecord = (props) => {
    return <div className="playerRecord container--pa container--styled">
        <h2>Player Record</h2>
        <div className="recordContainer">
            <div className="recordContainerRow flex flex-ai-c flex-jc-c">
                <div className="recordContainerRowLeft">
                    <img className="statValueTeam" src={props.player?.currentTeam?.teamLogo} alt="Team"/>
                    <p className="statTxt">{props.player?.currentTeam?.teamName}</p>
                </div>
                <div className="recordContainerRowRight">
                    <h4 className="statValue">{props.player?.playerRecord?.gamesPlayed}</h4>
                    <p className="statTxt">Games Played</p> 
                </div>
            </div>
            <div className="recordContainerRow flex flex-ai-c flex-jc-c">
                <div className="recordContainerRowLeft">
                    <h4 className="statValue">{props.player?.playerRecord?.goals}</h4>
                    <p className="statTxt">Goals</p> 
                </div>
                <div className="recordContainerRowRight">
                    <h4 className="statValue">{props.player?.playerRecord?.assists}</h4>
                    <p className="statTxt">Assists</p> 
                </div>
            </div>
        </div>
    </div>
}

export default PlayerRecord;