const PlayerRecord = (props) => {
    const varyingStat = () => {
        if(props.player?.position === "Attacker"){
            return <>
                <h4 className="statValue">
                    {
                        props.player?.playerRecord?.gamesPlayed !== 0 ? (props.player?.playerRecord?.goals + props.player?.playerRecord?.assists)
                        /(props.player?.playerRecord?.gamesPlayed) : 0
                    }
                </h4>
                <p className="statTxt">(G-A)/Game</p>
            </>
        }

        return <>
        <h4 className="statValue">
            {props.player?.playerRecord?.cleanSheets}
        </h4>
        <p className="statTxt">Clean sheets</p>
    </>
    }

    return <div className="playerRecord container--pa container--styled">
        <h2>Player Record</h2>
        <div className="recordContainer">
            <div className="recordContainerRow flex flex-ai-c flex-jc-c">
                <div className="recordContainerRowLeft">
                    {varyingStat()}
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