// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Sound/SoundBase.h"
#include "Components/AudioComponent.h"
#include "AustralianPrisonCook.h"
#include "SoundEffectPlayer.generated.h"

UCLASS()
class AUSTRALIANPRISONCOOK_API ASoundEffectPlayer : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	ASoundEffectPlayer();

protected:
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = "Audio")
	UAudioComponent* AudioComponent;

public:	
	UFUNCTION(BlueprintCallable, Category = "Sound")
	void PlaySoundEffect(USoundBase* Sound, FVector Location);

	//Play the sound, as long as it is not disabled
	UFUNCTION(BlueprintCallable, Category = "Sound")
	void PlaySoundEffectControlled(USoundBase* Sound);

	// Stop the sound
	UFUNCTION(BlueprintCallable, Category = "Sound")
	void StopSoundEffectControlled();
};
