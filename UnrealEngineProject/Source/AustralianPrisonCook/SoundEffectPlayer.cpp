// Fill out your copyright notice in the Description page of Project Settings.


#include "SoundEffectPlayer.h"
#include "Kismet/GameplayStatics.h"

// Sets default values
ASoundEffectPlayer::ASoundEffectPlayer()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = false;
    AudioComponent = CreateDefaultSubobject<UAudioComponent>(TEXT("AudioComponent"));
    AudioComponent->bAutoActivate = false;
    RootComponent = AudioComponent;

}

void ASoundEffectPlayer::PlaySoundEffect(USoundBase* Sound, FVector Location)
{
    if (Sound)
    {
        UGameplayStatics::PlaySoundAtLocation(this, Sound, Location);
        UE_LOG(LogTemp, Warning, TEXT("Played sound effect: %s"), *Sound->GetName());
    }
    else
    {
        UE_LOG(LogTemp, Warning, TEXT("No sound specified to play."));
    }
}

void ASoundEffectPlayer::PlaySoundEffectControlled(USoundBase* Sound)
{
    if (Sound)
    {
        AudioComponent->SetSound(Sound); // Assign the sound to the AudioComponent
        AudioComponent->Play();         // Play the sound
        UE_LOG(LogTemp, Warning, TEXT("Playing sound effect: %s"), *Sound->GetName());
    }
    else
    {
        UE_LOG(LogTemp, Warning, TEXT("No sound specified to play."));
    }
}

void ASoundEffectPlayer::StopSoundEffectControlled()
{
    if (AudioComponent->IsPlaying())
    {
        AudioComponent->Stop(); // Stop playback
        UE_LOG(LogTemp, Warning, TEXT("Stopped playing sound effect."));
    }
    else
    {
        UE_LOG(LogTemp, Warning, TEXT("No sound is currently playing."));
    }
}

