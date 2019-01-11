exports.get_deployment_settings = () => {
    const jsonValue = process.env['DEPLOY_SETTINGS_JSON']
    let settings;
    try {
        settings = JSON.parse(jsonValue)
    } catch (e) {
        throw `Value of "DEPLOY_SETTINGS_JSON" environment variable is not valid JSON.`
    }

    return settings
}

exports.get_docker_settings = () => {
    let settings
    try {
        settings = module.exports.get_deployment_settings()
    } catch (e) {
        return
    }

    let docker_options;
    for (const prop in settings) {
        for (const deployment of settings[prop]) {
            if (deployment.type === 'docker') {
                docker_options = deployment.options;
            }
        }
    }
    return docker_options
}
